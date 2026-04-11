using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static Task LoadIntoBufferAsync(this HttpContent httpContent, long maxBufferSize, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return httpContent.LoadIntoBufferAsync(maxBufferSize);
    }
}
