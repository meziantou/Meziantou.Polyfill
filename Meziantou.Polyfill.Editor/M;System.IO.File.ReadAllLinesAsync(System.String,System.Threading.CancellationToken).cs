// XML-DOC: M:System.IO.File.ReadAllLinesAsync(System.String,System.Threading.CancellationToken)
#if !NET10_0_OR_GREATER && !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

partial class PolyfillExtensions
{
    extension(File)
    {
        public static Task<string[]> ReadAllLinesAsync(string path, CancellationToken cancellationToken = default)
        {
            return File.ReadAllLinesAsync(path, Encoding.UTF8, cancellationToken);
        }
    }
}
#endif
