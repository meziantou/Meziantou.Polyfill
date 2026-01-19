// XML-DOC: M:System.IO.File.WriteAllBytesAsync(System.String,System.ReadOnlyMemory{System.Byte},System.Threading.CancellationToken)
#if !NET10_0_OR_GREATER
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

partial class PolyfillExtensions
{
    extension(File)
    {
        public static async Task WriteAllBytesAsync(string path, ReadOnlyMemory<byte> bytes, CancellationToken cancellationToken = default)
        {
#if NET7_0_OR_GREATER
            await using var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read, bufferSize: 1, useAsync: true);
#else
            using var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read, bufferSize: 1, useAsync: true);
#endif
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            await stream.WriteAsync(bytes, cancellationToken).ConfigureAwait(false);
#else
            var buffer = bytes.ToArray();
            await stream.WriteAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);
#endif
        }
    }
}
#endif
