using System.IO;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static Task<string?> ReadLineAsync(this StreamReader target)
    {
        return Task.FromResult(target.ReadLine());
    }
}