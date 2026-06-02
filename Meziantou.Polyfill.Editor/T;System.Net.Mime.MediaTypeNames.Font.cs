static partial class PolyfillExtensions
{
    extension(System.Net.Mime.MediaTypeNames)
    {
        public static MediaTypeNamesFontPolyfill Font => MediaTypeNamesFontPolyfill.Instance;
    }
}

internal sealed class MediaTypeNamesFontPolyfill
{
    public static readonly MediaTypeNamesFontPolyfill Instance = new();
    private MediaTypeNamesFontPolyfill() { }
}