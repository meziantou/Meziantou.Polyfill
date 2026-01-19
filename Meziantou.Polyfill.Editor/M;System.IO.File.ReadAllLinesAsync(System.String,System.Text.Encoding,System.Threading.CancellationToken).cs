// XML-DOC: M:System.IO.File.ReadAllLinesAsync(System.String,System.Text.Encoding,System.Threading.CancellationToken)
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
        public static async Task<string[]> ReadAllLinesAsync(string path, Encoding encoding, CancellationToken cancellationToken = default)
        {
            var lines = new List<string>();
            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 1, useAsync: true);
            using var reader = new StreamReader(stream, encoding);
            string? line;
            while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
                lines.Add(line);
            }

            return lines.ToArray();
        }
    }
}
#endif
