using System.Diagnostics;

static partial class PolyfillExtensions
{
    extension(System.Environment)
    {
        public static int ProcessId
        {
            get
            {
                using var process = Process.GetCurrentProcess();
                return process.Id;
            }
        }
    }
}