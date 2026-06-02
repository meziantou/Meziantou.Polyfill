static partial class PolyfillExtensions
{
    extension(System.Net.Mime.MediaTypeNames.Application)
    {
        public static string Wasm => "application/wasm";
    }
}