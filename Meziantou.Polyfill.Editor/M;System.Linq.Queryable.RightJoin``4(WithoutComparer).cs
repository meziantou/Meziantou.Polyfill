// XML-DOC: M:System.Linq.Queryable.RightJoin``4(System.Linq.IQueryable{``0},System.Collections.Generic.IEnumerable{``1},System.Linq.Expressions.Expression{System.Func{``0,``2}},System.Linq.Expressions.Expression{System.Func{``1,``2}},System.Linq.Expressions.Expression{System.Func{``0,``1,``3}})
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

static partial class PolyfillExtensions
{
    public static IQueryable<TResult> RightJoin<TOuter, TInner, TKey, TResult>(this IQueryable<TOuter> outer, IEnumerable<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter?, TInner, TResult>> resultSelector)
    {
        if (outer is null)
        {
            throw new ArgumentNullException(nameof(outer));
        }

        if (inner is null)
        {
            throw new ArgumentNullException(nameof(inner));
        }

        if (outerKeySelector is null)
        {
            throw new ArgumentNullException(nameof(outerKeySelector));
        }

        if (innerKeySelector is null)
        {
            throw new ArgumentNullException(nameof(innerKeySelector));
        }

        if (resultSelector is null)
        {
            throw new ArgumentNullException(nameof(resultSelector));
        }

        IQueryable<TInner>? queryableInner = inner as IQueryable<TInner>;
        var innerExpression = queryableInner is not null ? queryableInner.Expression : Expression.Constant(inner, typeof(IEnumerable<TInner>));
        return outer.Provider.CreateQuery<TResult>(
            Expression.Call(
                null,
                new Func<IQueryable<TOuter>, IEnumerable<TInner>, Expression<Func<TOuter, TKey>>, Expression<Func<TInner, TKey>>, Expression<Func<TOuter?, TInner, TResult>>, IQueryable<TResult>>(RightJoin).Method,
                outer.Expression, innerExpression, Expression.Quote(outerKeySelector), Expression.Quote(innerKeySelector), Expression.Quote(resultSelector)));
    }
}
