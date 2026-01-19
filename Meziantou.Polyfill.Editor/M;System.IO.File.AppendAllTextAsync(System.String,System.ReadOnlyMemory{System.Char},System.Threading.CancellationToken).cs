// XML-DOC: M:System.IO.File.AppendAllTextAsync(System.String,System.ReadOnlyMemory{System.Char},System.Threading.CancellationToken)
#if !NET10_0_OR_GREATER
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

partial class PolyfillExtensions
{
    extension(File)
    {
        public static async Task AppendAllTextAsync(string path, ReadOnlyMemory<char> contents, CancellationToken cancellationToken = default)
        {
#if NET7_0_OR_GREATER
            await using var stream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Read, bufferSize: 1, useAsync: true);
#else
            using var stream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Read, bufferSize: 1, useAsync: true);
#endif
            using var writer = new StreamWriter(stream, Encoding.UTF8);
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            await writer.WriteAsync(contents, cancellationToken).ConfigureAwait(false);
#else
            await writer.WriteAsync(contents.ToString()).ConfigureAwait(false);
#endif
        }
    }
}
#endif
