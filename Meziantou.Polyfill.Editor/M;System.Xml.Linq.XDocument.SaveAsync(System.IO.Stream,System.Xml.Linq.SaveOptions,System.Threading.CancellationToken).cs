#pragma warning disable CA1510 // Use ArgumentNullException throw helper

using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Xml.Linq;

static partial class PolyfillExtensions
{
    /// <summary>
    /// Asynchronously saves this <see cref="XDocument"/> to a <see cref="Stream"/>.
    /// </summary>
    /// <param name="document">The document to save.</param>
    /// <param name="stream">The stream to which to save the document.</param>
    /// <param name="options">A set of options that control how to save the document.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    public static
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    async
#endif
    Task SaveAsync(this XDocument document, Stream stream, SaveOptions options, CancellationToken cancellationToken)
    {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        if (document is null)
            throw new ArgumentNullException(nameof(document));
        if (stream is null)
            throw new ArgumentNullException(nameof(stream));

        var settings = new XmlWriterSettings
        {
            Async = true,
            OmitXmlDeclaration = (options & SaveOptions.OmitDuplicateNamespaces) != 0,
            Indent = (options & SaveOptions.DisableFormatting) == 0,
        };

        await using var writer = XmlWriter.Create(stream, settings);
        await document.SaveAsync(writer, cancellationToken).ConfigureAwait(false);
#else
        if (document is null)
            throw new ArgumentNullException(nameof(document));
        if (stream is null)
            throw new ArgumentNullException(nameof(stream));

        cancellationToken.ThrowIfCancellationRequested();
        document.Save(stream, options);
        return Task.CompletedTask;
#endif
    }
}
