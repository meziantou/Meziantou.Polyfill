using System.Collections;
using Xunit;

namespace Meziantou.Polyfill.Tests;

public class SystemCollectionsTests
{
    [Fact]
    public void BitArray_PopCount()
    {
        Assert.Equal(0, new BitArray(0).PopCount());
        Assert.Equal(3, new BitArray(new[] { true, false, true, true, false }).PopCount());

        var value = new BitArray(35, defaultValue: true);
        Assert.Equal(35, value.PopCount());

        value[0] = false;
        value[34] = false;
        Assert.Equal(33, value.PopCount());
    }
}
