// define-type System.Net.Mime.MediaTypeNames.Video
static partial class PolyfillExtensions
{
#if MEZIANTOU_POLYFILL_TYPE_SYSTEM_NET_MIME_MEDIATYPENAMES_VIDEO
    extension(System.Net.Mime.MediaTypeNames.Video)
    {
        public static string WebM => "video/webm";
    }
#else
    extension(MediaTypeNamesVideoPolyfill instance)
    {
        public string WebM => "video/webm";
    }
#endif
}
