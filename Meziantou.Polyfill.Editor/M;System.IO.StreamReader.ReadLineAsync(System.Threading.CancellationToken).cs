using System.IO;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<string?> ReadLineAsync(this StreamReader target, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return new(target.ReadLineAsync());
    }
}