using System.Runtime.CompilerServices;
static partial class PolyfillExtensions
{
    extension(ref DefaultInterpolatedStringHandler handler)
    {
        public void Clear() => handler = default;
    }
}
