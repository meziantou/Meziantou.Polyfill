static partial class PolyfillExtensions
{
    extension(System.Net.Mime.MediaTypeNames.Application)
    {
        public static string XmlDtd => "application/xml-dtd";
    }
}