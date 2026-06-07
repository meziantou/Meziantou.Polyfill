using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xunit;

namespace Meziantou.Polyfill.Tests;

public class RegexTests
{
    [Fact]
    public void CountAndIsMatch()
    {
        var regex = new Regex(@"\d+", RegexOptions.None, TimeSpan.FromSeconds(1));
        Assert.Equal(2, regex.Count("a1 b22".AsSpan()));
        Assert.Equal(2, Regex.Count("a1 b22".AsSpan(), @"\d+", RegexOptions.None, TimeSpan.FromSeconds(1)));
        Assert.True(regex.IsMatch("a1".AsSpan()));
        Assert.True(Regex.IsMatch("a1".AsSpan(), @"\d+", RegexOptions.None, TimeSpan.FromSeconds(1)));
    }

    [Fact]
    public void EnumerateMatchesAndSplits()
    {
        var regex = new Regex(@"\d+", RegexOptions.None, TimeSpan.FromSeconds(1));
        var matches = new List<(int Index, int Length)>();
        foreach (var match in regex.EnumerateMatches("a1 b22".AsSpan()))
            matches.Add((match.Index, match.Length));
        Assert.Equal([(1, 1), (4, 2)], matches);

        ReadOnlySpan<char> input = "a1b22c";
        var values = new List<string>();
        foreach (var range in regex.EnumerateSplits(input))
            values.Add(input[range].ToString());
        Assert.Equal(["a", "b", "c"], values);
    }
}
