// XML-DOC: M:System.IO.File.WriteAllLinesAsync(System.String,System.Collections.Generic.IEnumerable{System.String},System.Threading.CancellationToken)
#if !NET10_0_OR_GREATER && !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

partial class PolyfillExtensions
{
    extension(File)
    {
        public static Task WriteAllLinesAsync(string path, IEnumerable<string> contents, CancellationToken cancellationToken = default)
        {
            return File.WriteAllLinesAsync(path, contents, Encoding.UTF8, cancellationToken);
        }
    }
}
#endif
