using BlockchainAvalanche.Core;

namespace BlockchainAvalanche.Tests;

public class NetworkTests
{
    [Fact]
    public void Network_ShouldAddNodes()
    {
        var network = new Network();
        
        network.AddNode();
        network.AddNode();
        network.AddNode();
        
        Assert.Equal(3, network.Nodes.Count);
    }

    [Fact]
    public void Network_ShouldBroadcastTransactionToAllNodes()
    {
        var network = new Network();
        network.AddNode();
        network.AddNode();
        
        var tx = new Transaction("Alice", "Bob", 50);
        network.BroadcastTransaction(tx);
        
        foreach (var node in network.Nodes)
        {
            Assert.Single(node.PendingTransactions);
            Assert.Equal(tx.Id, node.PendingTransactions[0].Id);
        }
    }
}
