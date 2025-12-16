using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask DisposeAsync(this CancellationTokenRegistration registration)
    {
        registration.Dispose();
        return default;
    }
}
