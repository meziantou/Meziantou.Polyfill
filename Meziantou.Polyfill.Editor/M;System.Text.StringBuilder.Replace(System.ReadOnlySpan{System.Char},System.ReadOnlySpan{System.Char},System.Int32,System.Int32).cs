using System;
using System.Text;

static partial class PolyfillExtensions
{
    public static StringBuilder Replace(this StringBuilder target, ReadOnlySpan<char> oldValue, ReadOnlySpan<char> newValue, int startIndex, int count)
    {
        return target.Replace(oldValue.ToString(), newValue.ToString(), startIndex, count);
    }
}