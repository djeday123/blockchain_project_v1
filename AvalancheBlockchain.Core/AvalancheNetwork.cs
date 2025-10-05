namespace AvalancheBlockchain.Core;

/// <summary>
/// Network coordinator for Avalanche consensus
/// </summary>
public class AvalancheNetwork
{
    public List<Node> Nodes { get; private set; }
    private readonly SnowballConsensus _consensus;

    public AvalancheNetwork(int sampleSize = 5, double quorumThreshold = 0.6, int decisionThreshold = 10)
    {
        Nodes = new List<Node>();
        _consensus = new SnowballConsensus(sampleSize, quorumThreshold, decisionThreshold);
    }

    public void AddNode(Node node)
    {
        Nodes.Add(node);
    }

    /// <summary>
    /// Propagate a block through the network using Avalanche consensus
    /// </summary>
    public bool PropagateBlock(Block block, int maxRounds = 20)
    {
        // Each node receives the block and performs consensus
        foreach (var node in Nodes)
        {
            node.ReceiveBlock(block);
        }

        // Perform consensus rounds
        for (int round = 0; round < maxRounds; round++)
        {
            bool allDecided = true;

            foreach (var node in Nodes)
            {
                // Query other nodes
                bool querySuccess = _consensus.Query(Nodes, node, block.Hash);
                
                // Update preference based on query
                bool decided = _consensus.UpdatePreference(node, block.Hash, querySuccess);
                
                if (!decided)
                {
                    allDecided = false;
                }
            }

            // If all nodes decided, consensus is reached
            if (allDecided)
            {
                // Add block to all node blockchains
                foreach (var node in Nodes)
                {
                    if (!node.Blockchain.Chain.Any(b => b.Hash == block.Hash))
                    {
                        node.Blockchain.Chain.Add(block);
                    }
                }
                return true;
            }
        }

        return false; // Consensus not reached within max rounds
    }

    public void PrintNetworkStatus()
    {
        Console.WriteLine($"\n=== Network Status ({Nodes.Count} nodes) ===");
        foreach (var node in Nodes)
        {
            var pref = node.PreferredBlock != null 
                ? $"{node.PreferredBlock.BlockHash[..8]} (conf: {node.PreferredBlock.Confidence})"
                : "none";
            Console.WriteLine($"{node.Id}: Chain length={node.Blockchain.Chain.Count}, Preferred={pref}");
        }
    }
}
