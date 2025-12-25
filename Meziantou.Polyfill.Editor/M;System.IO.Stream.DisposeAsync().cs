using System;
using System.IO;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static async ValueTask DisposeAsync(this Stream target)
    {
        if (target is IAsyncDisposable asyncDisposable)
        {
            await asyncDisposable.DisposeAsync().ConfigureAwait(false);
        }
        else
        {
            target.Dispose();
        }
    }
}
