#pragma warning disable CA1510 // Use ArgumentNullException throw helper

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

static partial class PolyfillExtensions
{
    /// <summary>
    /// Writes this node to an <see cref="XmlWriter"/> asynchronously.
    /// </summary>
    /// <param name="node">The node to write.</param>
    /// <param name="writer">The XML writer to which to write the node.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    public static Task WriteToAsync(this XNode node, XmlWriter writer, CancellationToken cancellationToken)
    {
        if (node is null)
            throw new ArgumentNullException(nameof(node));
        if (writer is null)
            throw new ArgumentNullException(nameof(writer));

        cancellationToken.ThrowIfCancellationRequested();
        node.WriteTo(writer);
        return Task.CompletedTask;
    }
}
