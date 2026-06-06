using System.IO;
using System.Threading;
using System.Threading.Tasks;
static partial class PolyfillExtensions
{
    public static Task FlushAsync(this TextWriter target, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return Task.FromCanceled(cancellationToken);

        return target.FlushAsync();
    }
}
