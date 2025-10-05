using AvalancheBlockchain.Core;

namespace AvalancheBlockchain.Tests;

public class TransactionTests
{
    [Fact]
    public void Transaction_ShouldCalculateHash()
    {
        var tx = new Transaction("Alice", "Bob", 100);
        
        Assert.NotNull(tx.Id);
        Assert.NotEmpty(tx.Id);
    }

    [Fact]
    public void Transaction_ShouldHaveCorrectProperties()
    {
        var tx = new Transaction("Alice", "Bob", 50);
        
        Assert.Equal("Alice", tx.From);
        Assert.Equal("Bob", tx.To);
        Assert.Equal(50, tx.Amount);
    }
}
