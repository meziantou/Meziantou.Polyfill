using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static Task<string> ReadAsStringAsync(this HttpContent httpContent, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return httpContent.ReadAsStringAsync();
    }
}