using AvalancheBlockchain.Core;

namespace AvalancheBlockchain.Tests;

public class BlockchainTests
{
    [Fact]
    public void Blockchain_ShouldCreateGenesisBlock()
    {
        var blockchain = new Blockchain();
        
        Assert.Single(blockchain.Chain);
        Assert.Equal(0, blockchain.Chain[0].Index);
    }

    [Fact]
    public void Blockchain_ShouldAddTransactions()
    {
        var blockchain = new Blockchain();
        var tx = new Transaction("Alice", "Bob", 100);
        
        blockchain.AddTransaction(tx);
        
        Assert.Single(blockchain.PendingTransactions);
    }

    [Fact]
    public void Blockchain_ShouldMinePendingTransactions()
    {
        var blockchain = new Blockchain();
        blockchain.AddTransaction(new Transaction("Alice", "Bob", 100));
        
        blockchain.MinePendingTransactions("Miner");
        
        Assert.Equal(2, blockchain.Chain.Count);
    }

    [Fact]
    public void Blockchain_ShouldCalculateBalance()
    {
        var blockchain = new Blockchain();
        blockchain.AddTransaction(new Transaction("Alice", "Bob", 100));
        blockchain.MinePendingTransactions("Miner");
        
        var balance = blockchain.GetBalance("Bob");
        
        Assert.Equal(100, balance);
    }

    [Fact]
    public void Blockchain_ShouldValidateChain()
    {
        var blockchain = new Blockchain();
        blockchain.AddTransaction(new Transaction("Alice", "Bob", 100));
        blockchain.MinePendingTransactions("Miner");
        
        Assert.True(blockchain.IsChainValid());
    }

    [Fact]
    public void Blockchain_ShouldDetectInvalidChain()
    {
        var blockchain = new Blockchain();
        blockchain.AddTransaction(new Transaction("Alice", "Bob", 100));
        blockchain.MinePendingTransactions("Miner");
        
        // Tamper with a block
        blockchain.Chain[1].Transactions[0].Amount = 200;
        
        Assert.False(blockchain.IsChainValid());
    }
}
