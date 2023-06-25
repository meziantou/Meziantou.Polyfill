// when T:System.ValueTuple
// when T:System.ValueTuple`1
// when T:System.ValueTuple`2
// when T:System.ValueTuple`3
// when T:System.ValueTuple`4
// when T:System.ValueTuple`5
// when T:System.ValueTuple`6
// when T:System.ValueTuple`7
// when T:System.ValueTuple`8
using System.Collections;

namespace System;
internal interface ITupleInternal
{
    int GetHashCode(IEqualityComparer comparer);
    int Size { get; }
    string ToStringEnd();
}
