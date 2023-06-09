using System;
using System.Text;

static partial class PolyfillExtensions
{
    public static string Replace(this string target, string oldValue, string? newValue, StringComparison comparisonType)
    {
        if (oldValue == null)
            throw new ArgumentNullException(nameof(oldValue));

        if (oldValue == "")
            throw new ArgumentException("The value cannot be an empty string.", nameof(oldValue));

        var sb = new StringBuilder();

        var previousIndex = 0;
        while (target.IndexOf(oldValue, previousIndex, comparisonType) is var index and not -1)
        {
            sb.Append(target, previousIndex, index - previousIndex);
            sb.Append(newValue);
            previousIndex = index + oldValue.Length;
        }

        sb.Append(target, previousIndex, target.Length - previousIndex);
        return sb.ToString();
    }
}