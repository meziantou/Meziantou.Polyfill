// XML-DOC: M:System.IO.File.AppendAllTextAsync(System.String,System.String,System.Text.Encoding,System.Threading.CancellationToken)
#if !NET10_0_OR_GREATER && !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

partial class PolyfillExtensions
{
    extension(File)
    {
        public static async Task AppendAllTextAsync(string path, string? contents, Encoding encoding, CancellationToken cancellationToken = default)
        {
            if (contents == null)
                return;

            using var stream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Read, bufferSize: 1, useAsync: true);
            using var writer = new StreamWriter(stream, encoding);
            await writer.WriteAsync(contents).ConfigureAwait(false);
        }
    }
}
#endif
