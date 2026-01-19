// XML-DOC: M:System.IO.File.AppendAllLinesAsync(System.String,System.Collections.Generic.IEnumerable{System.String},System.Threading.CancellationToken)
#if !NET10_0_OR_GREATER
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

partial class PolyfillExtensions
{
    extension(File)
    {
        public static Task AppendAllLinesAsync(string path, IEnumerable<string> contents, CancellationToken cancellationToken = default)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            return File.AppendAllLinesAsync(path, contents, Encoding.UTF8, cancellationToken);
#else
            return AppendAllLinesAsyncCore(path, contents, Encoding.UTF8, cancellationToken);
#endif
        }

#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER
        private static async Task AppendAllLinesAsyncCore(string path, IEnumerable<string> contents, Encoding encoding, CancellationToken cancellationToken)
        {
            using var stream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Read, bufferSize: 1, useAsync: true);
            using var writer = new StreamWriter(stream, encoding);
            foreach (var line in contents)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await writer.WriteLineAsync(line).ConfigureAwait(false);
            }
        }
#endif
    }
}
#endif
