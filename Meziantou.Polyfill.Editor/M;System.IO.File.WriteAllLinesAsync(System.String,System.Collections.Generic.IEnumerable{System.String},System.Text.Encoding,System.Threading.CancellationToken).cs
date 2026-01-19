// XML-DOC: M:System.IO.File.WriteAllLinesAsync(System.String,System.Collections.Generic.IEnumerable{System.String},System.Text.Encoding,System.Threading.CancellationToken)
#if !NET10_0_OR_GREATER && !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

partial class PolyfillExtensions
{
    extension(File)
    {
        public static async Task WriteAllLinesAsync(string path, IEnumerable<string> contents, Encoding encoding, CancellationToken cancellationToken = default)
        {
            using var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read, bufferSize: 1, useAsync: true);
            using var writer = new StreamWriter(stream, encoding);
            foreach (var line in contents)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await writer.WriteLineAsync(line).ConfigureAwait(false);
            }
        }
    }
}
#endif
