﻿using System.Text;

static partial class PolyfillExtensions
{
    public static StringBuilder AppendJoin(this StringBuilder target, string? separator, params string?[] values)
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