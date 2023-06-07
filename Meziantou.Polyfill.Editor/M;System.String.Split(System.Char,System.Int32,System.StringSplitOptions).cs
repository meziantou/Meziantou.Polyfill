using System;

static partial class PolyfillExtensions
{
    public static string[] Split(this string target, char separator, int count, StringSplitOptions options = StringSplitOptions.None)
    {
        return target.Split(new char[] { separator }, count, options);
    }
}
