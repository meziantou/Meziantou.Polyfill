using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;

static partial class PolyfillExtensions
{
    public static void Kill(this Process process, bool entireProcessTree)
    {
        if (!entireProcessTree)
        {
            process.Kill();
            return;
        }

        ProcessKillEntireProcessTree.KillTree(process);
    }
}

file static class ProcessKillEntireProcessTree
{
    private const uint Th32csSnapProcess = 0x00000002;
    private const uint ProcessQueryLimitedInformation = 0x00001000;
    private const int ProcPidTBsdInfo = 3;
    private const int ProcBsdInfoSize = 136;
    private const int ProcBsdInfoParentPidOffset = 16;
    private const int Esrch = 3;

    private const int SourceWindows = 0;
    private const int SourceProcfs = 1; // Linux
    private const int SourceLibproc = 2; // macOS
    private const int SourcePs = 3; // Other Unix systems

    private static readonly int Source = GetSource();

    public static void KillTree(Process process)
    {
        var processId = process.Id; // Throws InvalidOperationException when no process is associated
        if (IsSelfOrAncestorOfCurrentProcess(processId))
            throw new InvalidOperationException("Cannot be used to terminate a process tree containing the calling process.");

        var exceptions = new List<Exception>();
        if (Source == SourceWindows)
        {
            KillTreeWindows(process, exceptions);
        }
        else
        {
            KillTreeUnix(process, exceptions);
        }

        if (exceptions.Count > 0)
            throw new AggregateException("Not all processes in process tree could be terminated.", exceptions);
    }

    private static int GetSource()
    {
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            return SourceWindows;

        if (File.Exists("/proc/self/stat"))
            return SourceProcfs;

        try
        {
            proc_listchildpids(1, null, 0);
            return SourceLibproc;
        }
        catch (DllNotFoundException)
        {
        }
        catch (EntryPointNotFoundException)
        {
        }

        return SourcePs;
    }

    private static bool IsSelfOrAncestorOfCurrentProcess(int processId)
    {
        using var currentProcess = Process.GetCurrentProcess();
        var current = currentProcess.Id;
        if (current == processId)
            return true;

        Dictionary<int, int>? parents = null;
        if (Source is SourceWindows or SourcePs)
        {
            parents = new Dictionary<int, int>();
            foreach (var (pid, parentPid) in GetProcessSnapshot())
            {
                parents[pid] = parentPid;
            }
        }

        // On Windows, the parent id is not updated when the parent exits, so the id may have been reused
        // by an unrelated process. A parent must have started before its child.
        var currentStartTime = Source == SourceWindows ? TryGetStartTime(currentProcess) : null;
        var visited = new HashSet<int> { current };
        while (true)
        {
            int parent;
            if (parents is not null)
            {
                if (!parents.TryGetValue(current, out parent))
                    return false;
            }
            else
            {
                var value = Source == SourceProcfs ? GetParentProcessIdProcfs(current) : GetParentProcessIdLibproc(current);
                if (value is null)
                    return false;

                parent = value.Value;
            }

            if (parent <= 0 || !visited.Add(parent))
                return false;

            if (Source == SourceWindows)
            {
                var parentStartTime = TryGetStartTime(parent);
                if (parentStartTime is null || currentStartTime is null || parentStartTime > currentStartTime)
                    return false;

                currentStartTime = parentStartTime;
            }

            if (parent == processId)
                return true;

            current = parent;
        }
    }

    private static void KillTreeWindows(Process process, List<Exception> exceptions)
    {
        // Keep a handle on the process so its id cannot be reused while its children are enumerated
        var handle = OpenProcess(ProcessQueryLimitedInformation, false, process.Id);
        if (handle == IntPtr.Zero)
            return;

        try
        {
            var startTime = TryGetStartTime(process);

            // Kill the process first, so it cannot create new children.
            // On Windows, children keep the id of their parent after it exits.
            KillProcess(process, exceptions);
            if (startTime is null)
                return;

            foreach (var child in GetChildProcesses(process.Id, startTime))
            {
                using (child)
                {
                    KillTreeWindows(child, exceptions);
                }
            }
        }
        finally
        {
            CloseHandle(handle);
        }
    }

    private static void KillTreeUnix(Process process, List<Exception> exceptions)
    {
        if (HasExited(process))
            return;

        // Children must be enumerated before the process is killed, as they are re-parented once it exits.
        // Stop the process first, so it cannot create new children while they are enumerated.
        // This is not done when using 'ps' as starting a process while a child process is stopped can hang.
        var processId = process.Id;
        List<Process> children;
        if (Source == SourcePs)
        {
            children = GetChildProcesses(processId, parentStartTime: null);
        }
        else
        {
            if (!TryStopProcess(processId, exceptions))
                return;

            children = GetChildProcesses(processId, parentStartTime: null);
        }

        KillProcess(process, exceptions);
        foreach (var child in children)
        {
            using (child)
            {
                KillTreeUnix(child, exceptions);
            }
        }
    }

    private static void KillProcess(Process process, List<Exception> exceptions)
    {
        try
        {
            process.Kill();
        }
        catch (InvalidOperationException)
        {
            // The process has already exited
        }
        catch (Win32Exception ex)
        {
            if (!HasExited(process))
            {
                exceptions.Add(ex);
            }
        }
    }

    private static bool HasExited(Process process)
    {
        try
        {
            return process.HasExited;
        }
        catch (InvalidOperationException)
        {
            return true;
        }
        catch (Win32Exception)
        {
            return false;
        }
        catch (NotSupportedException)
        {
            return false;
        }
    }

    private static DateTime? TryGetStartTime(int processId)
    {
        try
        {
            using var process = Process.GetProcessById(processId);
            return TryGetStartTime(process);
        }
        catch (ArgumentException)
        {
            return null;
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }

    private static DateTime? TryGetStartTime(Process process)
    {
        try
        {
            return process.StartTime;
        }
        catch (InvalidOperationException)
        {
            return null;
        }
        catch (Win32Exception)
        {
            return null;
        }
        catch (NotSupportedException)
        {
            return null;
        }
    }

    private static List<Process> GetChildProcesses(int parentProcessId, DateTime? parentStartTime)
    {
        var result = new List<Process>();
        foreach (var pid in GetChildProcessIds(parentProcessId))
        {
            if (pid == parentProcessId)
                continue;

            Process child;
            try
            {
                child = Process.GetProcessById(pid);
            }
            catch (ArgumentException)
            {
                continue; // The process has exited
            }
            catch (InvalidOperationException)
            {
                continue;
            }

            if (parentStartTime is not null)
            {
                var childStartTime = TryGetStartTime(child);
                if (childStartTime is null || childStartTime < parentStartTime)
                {
                    child.Dispose();
                    continue;
                }
            }

            result.Add(child);
        }

        return result;
    }

    private static List<int> GetChildProcessIds(int parentProcessId)
    {
        var result = new List<int>();
        if (Source == SourceLibproc)
        {
            var count = proc_listchildpids(parentProcessId, null, 0);
            while (count > 0)
            {
                var buffer = new int[count + 16];
                var actualCount = proc_listchildpids(parentProcessId, buffer, buffer.Length * sizeof(int));
                if (actualCount <= 0)
                    break;

                if (actualCount < buffer.Length)
                {
                    for (var i = 0; i < actualCount; i++)
                    {
                        result.Add(buffer[i]);
                    }

                    break;
                }

                count = actualCount;
            }

            return result;
        }

        foreach (var (pid, parentPid) in GetProcessSnapshot())
        {
            if (parentPid == parentProcessId)
            {
                result.Add(pid);
            }
        }

        return result;
    }

    private static List<(int ProcessId, int ParentProcessId)> GetProcessSnapshot()
    {
        return Source switch
        {
            SourceWindows => GetProcessSnapshotWindows(),
            SourceProcfs => GetProcessSnapshotProcfs(),
            _ => GetProcessSnapshotPs(),
        };
    }

    private static List<(int ProcessId, int ParentProcessId)> GetProcessSnapshotWindows()
    {
        var result = new List<(int ProcessId, int ParentProcessId)>();
        var snapshot = CreateToolhelp32Snapshot(Th32csSnapProcess, 0);
        if (snapshot == IntPtr.Zero || snapshot == new IntPtr(-1))
            throw new Win32Exception(Marshal.GetLastWin32Error());

        try
        {
            var entry = new ProcessEntry32 { dwSize = (uint)Marshal.SizeOf<ProcessEntry32>() };
            if (Process32FirstW(snapshot, ref entry))
            {
                do
                {
                    result.Add(((int)entry.th32ProcessID, (int)entry.th32ParentProcessID));
                }
                while (Process32NextW(snapshot, ref entry));
            }
        }
        finally
        {
            CloseHandle(snapshot);
        }

        return result;
    }

    private static List<(int ProcessId, int ParentProcessId)> GetProcessSnapshotProcfs()
    {
        var result = new List<(int ProcessId, int ParentProcessId)>();
        foreach (var directory in Directory.EnumerateDirectories("/proc"))
        {
            if (!int.TryParse(Path.GetFileName(directory), NumberStyles.None, CultureInfo.InvariantCulture, out var pid))
                continue;

            var parentPid = GetParentProcessIdProcfs(pid);
            if (parentPid is not null)
            {
                result.Add((pid, parentPid.Value));
            }
        }

        return result;
    }

    private static int? GetParentProcessIdProcfs(int processId)
    {
        string content;
        try
        {
            content = File.ReadAllText("/proc/" + processId.ToString(CultureInfo.InvariantCulture) + "/stat");
        }
        catch (IOException)
        {
            return null; // The process has exited
        }
        catch (UnauthorizedAccessException)
        {
            return null;
        }

        // Format: "pid (comm) state ppid ...". comm can contain spaces and parentheses.
        var index = content.LastIndexOf(')');
        if (index < 0)
            return null;

        var fields = content.Substring(index + 1).Split((char[]?)null, 3, StringSplitOptions.RemoveEmptyEntries);
        if (fields.Length >= 2 && int.TryParse(fields[1], NumberStyles.None, CultureInfo.InvariantCulture, out var parentPid))
            return parentPid;

        return null;
    }

    private static int? GetParentProcessIdLibproc(int processId)
    {
        var buffer = new byte[ProcBsdInfoSize];
        if (proc_pidinfo(processId, ProcPidTBsdInfo, 0, buffer, buffer.Length) != buffer.Length)
            return null;

        return BitConverter.ToInt32(buffer, ProcBsdInfoParentPidOffset);
    }

    private static List<(int ProcessId, int ParentProcessId)> GetProcessSnapshotPs()
    {
        var result = new List<(int ProcessId, int ParentProcessId)>();
        var psi = new ProcessStartInfo("ps", "-A -o pid= -o ppid=")
        {
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true,
        };

        using var ps = Process.Start(psi);
        if (ps is null)
            return result;

        var output = ps.StandardOutput.ReadToEnd();
        ps.WaitForExit();
        foreach (var line in output.Split('\n'))
        {
            var fields = line.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
            if (fields.Length == 2 &&
                int.TryParse(fields[0], NumberStyles.None, CultureInfo.InvariantCulture, out var pid) &&
                int.TryParse(fields[1], NumberStyles.None, CultureInfo.InvariantCulture, out var parentPid))
            {
                result.Add((pid, parentPid));
            }
        }

        return result;
    }

    private static bool TryStopProcess(int processId, List<Exception> exceptions)
    {
        var sigstop = Source == SourceProcfs ? 19 : 17;

        int result;
        try
        {
            result = SysKill(processId, sigstop);
        }
        catch (DllNotFoundException)
        {
            return true; // Best effort: continue without stopping the process
        }
        catch (EntryPointNotFoundException)
        {
            return true;
        }

        if (result == 0)
            return true;

        var error = Marshal.GetLastWin32Error();
        if (error != Esrch)
        {
            exceptions.Add(new Win32Exception(error));
        }

        return false;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct ProcessEntry32
    {
        public uint dwSize;
        public uint cntUsage;
        public uint th32ProcessID;
        public IntPtr th32DefaultHeapID;
        public uint th32ModuleID;
        public uint cntThreads;
        public uint th32ParentProcessID;
        public int pcPriClassBase;
        public uint dwFlags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szExeFile;
    }

#if MEZIANTOU_POLYFILL_SUPPORT_UPDATED_MEMORY_SAFETY_RULES
    [DllImport("kernel32.dll", SetLastError = true)]
    private static safe extern IntPtr CreateToolhelp32Snapshot(uint flags, uint processId);

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static safe extern bool Process32FirstW(IntPtr snapshot, ref ProcessEntry32 entry);

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static safe extern bool Process32NextW(IntPtr snapshot, ref ProcessEntry32 entry);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static safe extern IntPtr OpenProcess(uint desiredAccess, bool inheritHandle, int processId);

    [DllImport("kernel32.dll")]
    private static safe extern bool CloseHandle(IntPtr handle);

    [DllImport("libc", EntryPoint = "kill", SetLastError = true)]
    private static safe extern int SysKill(int pid, int signal);

    [DllImport("libproc", SetLastError = true)]
    private static safe extern int proc_listchildpids(int ppid, [Out] int[]? buffer, int bufferSize);

    [DllImport("libproc", SetLastError = true)]
    private static safe extern int proc_pidinfo(int pid, int flavor, ulong arg, [Out] byte[] buffer, int bufferSize);
#else
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr CreateToolhelp32Snapshot(uint flags, uint processId);

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern bool Process32FirstW(IntPtr snapshot, ref ProcessEntry32 entry);

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern bool Process32NextW(IntPtr snapshot, ref ProcessEntry32 entry);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr OpenProcess(uint desiredAccess, bool inheritHandle, int processId);

    [DllImport("kernel32.dll")]
    private static extern bool CloseHandle(IntPtr handle);

    [DllImport("libc", EntryPoint = "kill", SetLastError = true)]
    private static extern int SysKill(int pid, int signal);

    [DllImport("libproc", SetLastError = true)]
    private static extern int proc_listchildpids(int ppid, [Out] int[]? buffer, int bufferSize);

    [DllImport("libproc", SetLastError = true)]
    private static extern int proc_pidinfo(int pid, int flavor, ulong arg, [Out] byte[] buffer, int bufferSize);
#endif
}
