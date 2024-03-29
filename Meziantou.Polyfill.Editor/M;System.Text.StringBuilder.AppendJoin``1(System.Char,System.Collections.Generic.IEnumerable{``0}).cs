﻿using System.Collections.Generic;
using System.Text;

static partial class PolyfillExtensions
{
    public static StringBuilder AppendJoin<T>(this StringBuilder target, char separator, IEnumerable<T> values)
    {
        var first = true;
        foreach (var value in values)
        {
            if (!first)
            {
                target.Append(separator);
            }

            target.Append(value);
            first = false;
        }

        return target;
    }
}