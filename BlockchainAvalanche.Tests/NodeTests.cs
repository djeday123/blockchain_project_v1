using BlockchainAvalanche.Core;

namespace BlockchainAvalanche.Tests;

public class NodeTests
{
    [Fact]
    public void Node_ShouldCreateGenesisBlock()
    {
        var node = new Node("test-node");
        
        Assert.Single(node.Chain);
        Assert.Equal(0, node.Chain[0].Index);
    }

    [Fact]
    public void Node_ShouldAddTransaction()
    {
        var node = new Node("test-node");
        var tx = new Transaction("Alice", "Bob", 50);
        
        node.AddTransaction(tx);
        
        Assert.Single(node.PendingTransactions);
    }

    [Fact]
    public void Node_ShouldMinePendingTransactions()
    {
        var node = new Node("test-node", miningDifficulty: 1);
        node.AddTransaction(new Transaction("Alice", "Bob", 50));
        
        var block = node.MinePendingTransactions("Miner");
        
        Assert.NotNull(block);
        Assert.Equal(2, node.Chain.Count);
    }

    [Fact]
    public void Node_ShouldValidateChain()
    {
        var node = new Node("test-node", miningDifficulty: 1);
        node.AddTransaction(new Transaction("Alice", "Bob", 50));
        node.MinePendingTransactions("Miner");
        
        Assert.True(node.IsChainValid());
    }

    [Fact]
    public void Node_ShouldCalculateBalance()
    {
        var node = new Node("test-node", miningDifficulty: 1);
        node.AddTransaction(new Transaction("system", "Alice", 100));
        node.MinePendingTransactions("Miner");
        
        node.AddTransaction(new Transaction("Alice", "Bob", 30));
        node.MinePendingTransactions("Miner");
        
        var aliceBalance = node.GetBalance("Alice");
        var bobBalance = node.GetBalance("Bob");
        
        Assert.Equal(70, aliceBalance);
        Assert.Equal(30, bobBalance);
    }

    [Fact]
    public void Node_ShouldRejectInvalidTransaction()
    {
        var node = new Node("test-node");
        var tx = new Transaction("", "Bob", 50);
        
        Assert.Throws<ArgumentException>(() => node.AddTransaction(tx));
    }
}
