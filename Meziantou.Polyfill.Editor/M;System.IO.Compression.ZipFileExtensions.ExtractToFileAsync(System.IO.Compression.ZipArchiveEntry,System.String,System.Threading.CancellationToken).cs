using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static Task ExtractToFileAsync(this ZipArchiveEntry source, string destinationFileName, CancellationToken cancellationToken = default) =>
         ExtractToFileAsync(source, destinationFileName, false, cancellationToken);

}