// XML-DOC: M:System.IO.File.ReadLinesAsync(System.String,System.Threading.CancellationToken)
#if !NET10_0_OR_GREATER
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

partial class PolyfillExtensions
{
    extension(File)
    {
        public static IAsyncEnumerable<string> ReadLinesAsync(string path, CancellationToken cancellationToken = default)
        {
#if NET8_0_OR_GREATER
            return File.ReadLinesAsync(path, Encoding.UTF8, cancellationToken);
#else
            return ReadLinesAsyncCore(path, Encoding.UTF8, cancellationToken);
#endif
        }

#if !NET8_0_OR_GREATER
        private static async IAsyncEnumerable<string> ReadLinesAsyncCore(string path, Encoding encoding, [EnumeratorCancellation] CancellationToken cancellationToken)
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
#endif
    }
}
#endif
