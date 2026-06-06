using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    extension(ValueTask)
    {
        public static ValueTask<T> FromResult<T>(T result)
        {
            return new ValueTask<T>(result);
        }
    }
}
