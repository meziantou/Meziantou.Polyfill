// XML-DOC: M:System.IO.File.Move(System.String,System.String,System.Boolean)
#if !NET10_0_OR_GREATER && !NETCOREAPP3_0_OR_GREATER && !NETSTANDARD2_1_OR_GREATER
using System.IO;

partial class PolyfillExtensions
{
    extension(File)
    {
        public static void Move(string sourceFileName, string destFileName, bool overwrite)
        {
            if (overwrite && File.Exists(destFileName))
            {
                File.Delete(destFileName);
            }

            File.Move(sourceFileName, destFileName);
        }
    }
}
#endif
