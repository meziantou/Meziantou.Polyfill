static partial class PolyfillExtensions
{
    public static string ReplaceLineEndings(this string target)
    {
        return target.ReplaceLineEndings(System.Environment.NewLine);
    }
}