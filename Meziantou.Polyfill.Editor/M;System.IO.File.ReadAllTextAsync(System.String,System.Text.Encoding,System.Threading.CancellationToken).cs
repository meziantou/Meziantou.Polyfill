// XML-DOC: M:System.IO.File.ReadAllTextAsync(System.String,System.Text.Encoding,System.Threading.CancellationToken)
#if !NET10_0_OR_GREATER && !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

partial class PolyfillExtensions
{
    extension(File)
    {
        public static async Task<string> ReadAllTextAsync(string path, Encoding encoding, CancellationToken cancellationToken = default)
        {
            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 1, useAsync: true);
            using var reader = new StreamReader(stream, encoding);
            return await reader.ReadToEndAsync().ConfigureAwait(false);
        }
    }
}
#endif
