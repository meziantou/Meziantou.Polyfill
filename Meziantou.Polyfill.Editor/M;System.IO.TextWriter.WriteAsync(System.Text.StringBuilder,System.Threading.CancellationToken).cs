using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
static partial class PolyfillExtensions
{
    public static Task WriteAsync(this TextWriter target, StringBuilder? value, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return Task.FromCanceled(cancellationToken);

        return target.WriteAsync(value?.ToString());
    }
}
