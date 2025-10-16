using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    extension(File)
    {
        public static Task WriteAllTextAsync(string path, ReadOnlyMemory<char> contents, Encoding encoding, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
                return Task.FromCanceled(cancellationToken);

            System.IO.File.WriteAllText(path, contents.ToString(), encoding);
            return Task.CompletedTask;
        }
    }
}
