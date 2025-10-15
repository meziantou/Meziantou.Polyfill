using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static Task ExtractToFileAsync(this ZipArchiveEntry source, string destinationFileName, bool overwrite, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
            return Task.FromCanceled(cancellationToken);

        source.ExtractToFile(destinationFileName, overwrite);
        return Task.CompletedTask;
    }
}