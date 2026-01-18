#pragma warning disable CA1510 // Use ArgumentNullException throw helper

using System.Threading;
using System.Threading.Tasks;

namespace System.Xml.Linq;

static partial class PolyfillExtensions
{
    /// <summary>
    /// Asynchronously saves this <see cref="XDocument"/> to an <see cref="XmlWriter"/>.
    /// </summary>
    /// <param name="document">The document to save.</param>
    /// <param name="writer">The XML writer to which to save the document.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    public static
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    async
#endif
    Task SaveAsync(this XDocument document, XmlWriter writer, CancellationToken cancellationToken)
    {
        if (document is null)
            throw new ArgumentNullException(nameof(document));
        if (writer is null)
            throw new ArgumentNullException(nameof(writer));

        cancellationToken.ThrowIfCancellationRequested();
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        await document.WriteToAsync(writer, cancellationToken).ConfigureAwait(false);
        await writer.FlushAsync().ConfigureAwait(false);
#else
        document.WriteTo(writer);
        writer.Flush();
        return Task.CompletedTask;
#endif
    }
}
