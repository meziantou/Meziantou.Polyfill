// XML-DOC: M:System.IO.File.WriteAllTextAsync(System.String,System.String,System.Threading.CancellationToken)
#if !NET10_0_OR_GREATER && !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

partial class PolyfillExtensions
{
    extension(File)
    {
        public static Task WriteAllTextAsync(string path, string? contents, CancellationToken cancellationToken = default)
        {
            return File.WriteAllTextAsync(path, contents, Encoding.UTF8, cancellationToken);
        }
    }
}
#endif
