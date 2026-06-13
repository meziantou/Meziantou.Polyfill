static partial class PolyfillExtensions
{
    extension(System.Net.Mime.MediaTypeNames)
    {
        public static MediaTypeNamesVideoPolyfill Video => MediaTypeNamesVideoPolyfill.Instance;
    }
}

internal sealed class MediaTypeNamesVideoPolyfill
{
    public static readonly MediaTypeNamesVideoPolyfill Instance = new();
    private MediaTypeNamesVideoPolyfill() { }
}
