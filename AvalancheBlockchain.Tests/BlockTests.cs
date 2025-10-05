using AvalancheBlockchain.Core;

namespace AvalancheBlockchain.Tests;

public class BlockTests
{
    [Fact]
    public void Block_ShouldCalculateHash()
    {
        var transactions = new List<Transaction>
        {
            new Transaction("Alice", "Bob", 100)
        };
        var block = new Block(0, transactions, "0");
        
        Assert.NotNull(block.Hash);
        Assert.NotEmpty(block.Hash);
    }

    [Fact]
    public void Block_HashShouldChangeWhenNonceChanges()
    {
        var block = new Block(0, new List<Transaction>(), "0");
        var hash1 = block.Hash;
        
        block.Nonce = 1;
        var hash2 = block.CalculateHash();
        
        Assert.NotEqual(hash1, hash2);
    }
}
