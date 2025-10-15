using System.Diagnostics;

static partial class PolyfillExtensions
{
    extension(System.Environment)
    {
        public static int ProcessId
        {
            get
            {
                return Process.GetCurrentProcess().Id;
            }
        }
    }
}