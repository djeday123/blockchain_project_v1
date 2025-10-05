using BlockchainAvalanche.Core;

namespace BlockchainAvalanche.Tests;

public class BlockTests
{
    [Fact]
    public void Block_ShouldCalculateHash()
    {
        var transactions = new List<Transaction>
        {
            new Transaction("Alice", "Bob", 50)
        };
        var block = new Block(1, transactions, "previous-hash");
        
        Assert.NotNull(block.Hash);
        Assert.NotEmpty(block.Hash);
    }

    [Fact]
    public void Block_HashShouldChangeWithNonce()
    {
        var block = new Block(1, new List<Transaction>(), "previous-hash");
        var originalHash = block.Hash;
        
        block.Nonce = 1;
        var newHash = block.CalculateHash();
        
        Assert.NotEqual(originalHash, newHash);
    }

    [Fact]
    public void Block_MiningProducesHashWithCorrectDifficulty()
    {
        var block = new Block(1, new List<Transaction>(), "previous-hash");
        int difficulty = 2;
        
        block.MineBlock(difficulty);
        
        Assert.StartsWith("00", block.Hash);
    }
}
