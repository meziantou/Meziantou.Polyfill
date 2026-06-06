using System.IO;
using System.Threading;
using System.Threading.Tasks;
static partial class PolyfillExtensions
{
    public static Task<string?> ReadLineAsync(this TextReader target, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return Task.FromCanceled<string?>(cancellationToken);

        return target.ReadLineAsync();
    }
}
