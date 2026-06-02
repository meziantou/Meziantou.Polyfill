static partial class PolyfillExtensions
{
    extension(System.Net.Mime.MediaTypeNames.Application)
    {
        public static string XmlPatch => "application/xml-patch+xml";
    }
}