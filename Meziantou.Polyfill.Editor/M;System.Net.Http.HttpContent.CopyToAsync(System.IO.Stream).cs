using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static Task CopyToAsync(this HttpContent target, Stream stream) => target.CopyToAsync(stream, CancellationToken.None);
}
