using System.Threading.Tasks;
using Xunit;

namespace Meziantou.Polyfill.Tests;

public class ValueTaskTests
{
    [Fact]
    public void ValueTask_CompletedTask()
    {
        var completedTask = ValueTask.CompletedTask;

        Assert.True(completedTask.IsCompleted);
        Assert.True(completedTask.IsCompletedSuccessfully);
    }

    [Fact]
    public async Task ValueTask_CompletedTask_CanAwait()
    {
        await ValueTask.CompletedTask;

        // Should complete without throwing
        Assert.True(true);
    }
}
