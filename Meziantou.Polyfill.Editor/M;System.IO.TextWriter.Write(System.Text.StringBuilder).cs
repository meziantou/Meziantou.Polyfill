using System.IO;
using System.Text;
static partial class PolyfillExtensions
{
    public static void Write(this TextWriter target, StringBuilder? value) => target.Write(value?.ToString());
}
