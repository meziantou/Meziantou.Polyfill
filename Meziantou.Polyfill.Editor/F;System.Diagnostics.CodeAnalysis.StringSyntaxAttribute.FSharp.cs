#pragma warning disable CS0436 // Type conflicts with imported type
static partial class PolyfillExtensions
{
    extension(System.Diagnostics.CodeAnalysis.StringSyntaxAttribute)
    {
        public static string FSharp => "F#";
    }
}
