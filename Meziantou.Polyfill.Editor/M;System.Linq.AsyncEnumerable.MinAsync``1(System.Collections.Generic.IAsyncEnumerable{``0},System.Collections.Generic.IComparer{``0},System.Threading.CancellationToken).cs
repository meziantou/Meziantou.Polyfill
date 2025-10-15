using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<TSource?> MinAsync<TSource>(
        this IAsyncEnumerable<TSource> source,
        IComparer<TSource>? comparer = null,
        CancellationToken cancellationToken = default)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        comparer ??= Comparer<TSource>.Default;

        // Special-case float/double/float?/double? to maintain compatibility
        // with System.Linq.Enumerable implementations.
#pragma warning disable CA2012 // Use ValueTasks correctly
        if (typeof(TSource) == typeof(float) && comparer == Comparer<TSource>.Default)
        {
            return (ValueTask<TSource?>)(object)Helpers.MinAsync((IAsyncEnumerable<float>)(object)source, cancellationToken);
        }

        if (typeof(TSource) == typeof(double) && comparer == Comparer<TSource>.Default)
        {
            return (ValueTask<TSource?>)(object)Helpers.MinAsync((IAsyncEnumerable<double>)(object)source, cancellationToken);
        }

        if (typeof(TSource) == typeof(float?) && comparer == Comparer<TSource>.Default)
        {
            return (ValueTask<TSource?>)(object)Helpers.MinAsync((IAsyncEnumerable<float?>)(object)source, cancellationToken);
        }

        if (typeof(TSource) == typeof(double?) && comparer == Comparer<TSource>.Default)
        {
            return (ValueTask<TSource?>)(object)Helpers.MinAsync((IAsyncEnumerable<double?>)(object)source, cancellationToken);
        }
#pragma warning restore CA2012

        return Impl(source, comparer, cancellationToken);

        static async ValueTask<TSource?> Impl(IAsyncEnumerable<TSource> source, IComparer<TSource> comparer, CancellationToken cancellationToken)
        {
            await using IAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);

            TSource? value = default;
            if (default(TSource) is null)
            {
                do
                {
                    if (!await e.MoveNextAsync())
                    {
                        return value;
                    }

                    value = e.Current;
                }
                while (value is null);

                while (await e.MoveNextAsync())
                {
                    TSource next = e.Current;
                    if (next is not null && comparer.Compare(next, value) < 0)
                    {
                        value = next;
                    }
                }
            }
            else
            {
                if (!await e.MoveNextAsync())
                {
                    throw new InvalidOperationException("Sequence contains no elements");
                }

                value = e.Current;
                if (comparer == Comparer<TSource>.Default)
                {
                    while (await e.MoveNextAsync())
                    {
                        TSource next = e.Current;
                        if (Comparer<TSource>.Default.Compare(next, value) < 0)
                        {
                            value = next;
                        }
                    }
                }
                else
                {
                    while (await e.MoveNextAsync())
                    {
                        TSource next = e.Current;
                        if (comparer.Compare(next, value) < 0)
                        {
                            value = next;
                        }
                    }
                }
            }

            return value;
        }
    }
}

file static class Helpers
{
    public static async ValueTask<float> MinAsync(
          IAsyncEnumerable<float> source,
          CancellationToken cancellationToken)
    {
        await using IAsyncEnumerator<float> e = source.GetAsyncEnumerator(cancellationToken);

        if (!await e.MoveNextAsync())
        {
            throw new InvalidOperationException("Sequence contains no elements");
        }

        float value = e.Current;
        if (float.IsNaN(value))
        {
            return value;
        }

        while (await e.MoveNextAsync())
        {
            float x = e.Current;
            if (x < value)
            {
                value = x;
            }

            // Normally NaN < anything is false, as is anything < NaN
            // However, this leads to some irksome outcomes in Min and Max.
            // If we use those semantics then Min(NaN, 5.0) is NaN, but
            // Min(5.0, NaN) is 5.0!  To fix this, we impose a total
            // ordering where NaN is smaller than every value, including
            // negative infinity. Not testing for NaN therefore isn't an option, but since we
            // can't find a smaller value, we can short-circuit.
            else if (float.IsNaN(x))
            {
                return x;
            }
        }

        return value;
    }

    /// <summary>Returns the minimum value in a sequence of values.</summary>
    /// <param name="source">A sequence of values to determine the minimum value of.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests. The default is <see cref="CancellationToken.None"/>.</param>
    /// <returns>The minimum value in the sequence.</returns>
    public static async ValueTask<double> MinAsync(
        IAsyncEnumerable<double> source,
        CancellationToken cancellationToken)
    {
        await using IAsyncEnumerator<double> e = source.GetAsyncEnumerator(cancellationToken);

        if (!await e.MoveNextAsync())
        {
            throw new InvalidOperationException("Sequence contains no elements");
        }

        double value = e.Current;
        if (double.IsNaN(value))
        {
            return value;
        }

        while (await e.MoveNextAsync())
        {
            double x = e.Current;
            if (x < value)
            {
                value = x;
            }

            // Normally NaN < anything is false, as is anything < NaN
            // However, this leads to some irksome outcomes in Min and Max.
            // If we use those semantics then Min(NaN, 5.0) is NaN, but
            // Min(5.0, NaN) is 5.0!  To fix this, we impose a total
            // ordering where NaN is smaller than every value, including
            // negative infinity. Not testing for NaN therefore isn't an option, but since we
            // can't find a smaller value, we can short-circuit.
            else if (double.IsNaN(x))
            {
                return x;
            }
        }

        return value;
    }

    /// <summary>Returns the minimum value in a sequence of nullable values.</summary>
    /// <param name="source">A sequence of nullable values to determine the minimum value of.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests. The default is <see cref="CancellationToken.None"/>.</param>
    /// <returns>The minimum value in the sequence.</returns>
    public static async ValueTask<float?> MinAsync(
        IAsyncEnumerable<float?> source,
        CancellationToken cancellationToken)
    {
        float? value = null;
        await foreach (float? x in source.WithCancellation(cancellationToken))
        {
            if (x is null)
            {
                continue;
            }

            if (value == null || x < value || float.IsNaN(x.GetValueOrDefault()))
            {
                value = x;
            }
        }

        return value;
    }

    /// <summary>Returns the minimum value in a sequence of nullable values.</summary>
    /// <param name="source">A sequence of nullable values to determine the minimum value of.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests. The default is <see cref="CancellationToken.None"/>.</param>
    /// <returns>The minimum value in the sequence.</returns>
    public static async ValueTask<double?> MinAsync(
        IAsyncEnumerable<double?> source,
        CancellationToken cancellationToken)
    {
        double? value = null;
        await foreach (double? x in source.WithCancellation(cancellationToken))
        {
            if (x is null)
            {
                continue;
            }

            if (value == null || x < value || double.IsNaN(x.GetValueOrDefault()))
            {
                value = x;
            }
        }

        return value;
    }
}