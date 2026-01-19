// XML-DOC: M:System.IO.File.ReadLinesAsync(System.String,System.Text.Encoding,System.Threading.CancellationToken)
#if !NET10_0_OR_GREATER && !NET8_0_OR_GREATER
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

partial class PolyfillExtensions
{
    extension(File)
    {
        public static async IAsyncEnumerable<string> ReadLinesAsync(string path, Encoding encoding, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
#if NET7_0_OR_GREATER
            await using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 1, useAsync: true);
#else
            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 1, useAsync: true);
#endif
            using var reader = new StreamReader(stream, encoding);
            string? line;
            while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return line;
            }
        }
    }
}
#endif
