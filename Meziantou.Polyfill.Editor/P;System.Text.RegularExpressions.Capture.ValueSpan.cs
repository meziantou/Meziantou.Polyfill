using System;
using System.Text.RegularExpressions;

static partial class PolyfillExtensions
{
    extension(Capture target)
    {
        public ReadOnlySpan<char> ValueSpan => target.Value.AsSpan();
    }
}
