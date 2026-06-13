// define-type System.Net.Mime.MediaTypeNames.Video
static partial class PolyfillExtensions
{
#if MEZIANTOU_POLYFILL_TYPE_SYSTEM_NET_MIME_MEDIATYPENAMES_VIDEO
    extension(System.Net.Mime.MediaTypeNames.Video)
    {
        public static string Mp4 => "video/mp4";
    }
#else
    extension(MediaTypeNamesVideoPolyfill instance)
    {
        public string Mp4 => "video/mp4";
    }
#endif
}
