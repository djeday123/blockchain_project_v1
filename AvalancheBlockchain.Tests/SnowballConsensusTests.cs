using AvalancheBlockchain.Core;

namespace AvalancheBlockchain.Tests;

public class SnowballConsensusTests
{
    [Fact]
    public void SnowballConsensus_ShouldReachConsensusWithSufficientNodes()
    {
        var consensus = new SnowballConsensus(sampleSize: 3, quorumThreshold: 0.6, decisionThreshold: 5);
        var nodes = new List<Node>();
        
        for (int i = 0; i < 10; i++)
        {
            var node = new Node($"Node{i}", $"192.168.1.{i}");
            node.PreferredBlock = new BlockPreference("test-hash");
            nodes.Add(node);
        }

        var currentNode = new Node("TestNode", "192.168.1.100");
        currentNode.PreferredBlock = new BlockPreference("test-hash");
        
        bool queryResult = consensus.Query(nodes, currentNode, "test-hash");
        
        Assert.True(queryResult);
    }

    [Fact]
    public void SnowballConsensus_ShouldUpdatePreference()
    {
        var consensus = new SnowballConsensus();
        var node = new Node("Node1", "192.168.1.1");
        
        consensus.UpdatePreference(node, "test-hash", true);
        
        Assert.NotNull(node.PreferredBlock);
        Assert.Equal("test-hash", node.PreferredBlock.BlockHash);
        Assert.Equal(1, node.PreferredBlock.Confidence);
    }
}
