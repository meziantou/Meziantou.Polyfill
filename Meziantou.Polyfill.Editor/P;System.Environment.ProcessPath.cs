using System.Diagnostics;

static partial class PolyfillExtensions
{
    extension(System.Environment)
    {
        public static string? ProcessPath => EnvironmentProcessPath.Value;
    }
}

file static class EnvironmentProcessPath
{
    public static readonly string? Value = GetProcessPath();

    private static string? GetProcessPath()
    {
        try
        {
            using var process = Process.GetCurrentProcess();
            return process.MainModule?.FileName;
        }
        catch
        {
            return null;
        }
    }
}
