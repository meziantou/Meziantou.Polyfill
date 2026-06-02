static partial class PolyfillExtensions
{
    extension(System.Net.Mime.MediaTypeNames)
    {
        public static MediaTypeNamesMultipartPolyfill Multipart => MediaTypeNamesMultipartPolyfill.Instance;
    }
}

internal sealed class MediaTypeNamesMultipartPolyfill
{
    public static readonly MediaTypeNamesMultipartPolyfill Instance = new();
    private MediaTypeNamesMultipartPolyfill() { }
}