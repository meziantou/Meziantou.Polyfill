using System.IO;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static Task CopyToAsync(this Stream target, Stream destination, CancellationToken cancellationToken)
    {
        return target.CopyToAsync(destination, 81920, cancellationToken);
    }
}
