using BlockchainAvalanche.Core;

namespace BlockchainAvalanche.Tests;

public class TransactionTests
{
    [Fact]
    public void Transaction_ShouldGenerateUniqueId()
    {
        var tx1 = new Transaction("Alice", "Bob", 50);
        var tx2 = new Transaction("Alice", "Bob", 50);
        
        Assert.NotEqual(tx1.Id, tx2.Id);
    }

    [Fact]
    public void Transaction_ShouldHaveCorrectProperties()
    {
        var tx = new Transaction("Alice", "Bob", 50);
        
        Assert.Equal("Alice", tx.Sender);
        Assert.Equal("Bob", tx.Recipient);
        Assert.Equal(50, tx.Amount);
        Assert.NotNull(tx.Id);
    }
}
