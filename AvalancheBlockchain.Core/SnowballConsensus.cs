namespace AvalancheBlockchain.Core;

/// <summary>
/// Represents a vote preference for a block in the Avalanche consensus
/// </summary>
public class BlockPreference
{
    public string BlockHash { get; set; }
    public int Confidence { get; set; }
    public int ConsecutiveSuccesses { get; set; }

    public BlockPreference(string blockHash)
    {
        BlockHash = blockHash;
        Confidence = 0;
        ConsecutiveSuccesses = 0;
    }
}

/// <summary>
/// Implements the Snowball consensus protocol from Avalanche
/// This is a simplified educational version
/// </summary>
public class SnowballConsensus
{
    private readonly int _sampleSize;
    private readonly double _quorumThreshold;
    private readonly int _decisionThreshold;
    private readonly Random _random;

    public SnowballConsensus(int sampleSize = 5, double quorumThreshold = 0.6, int decisionThreshold = 10)
    {
        _sampleSize = sampleSize;
        _quorumThreshold = quorumThreshold;
        _decisionThreshold = decisionThreshold;
        _random = new Random();
    }

    /// <summary>
    /// Perform consensus query on a set of nodes
    /// </summary>
    public bool Query(List<Node> allNodes, Node currentNode, string blockHash)
    {
        if (allNodes.Count < _sampleSize)
        {
            return true; // Not enough nodes for consensus
        }

        // Sample random nodes
        var sampledNodes = SampleNodes(allNodes, currentNode, _sampleSize);
        
        // Count votes
        int votes = 0;
        foreach (var node in sampledNodes)
        {
            if (node.PreferredBlock?.BlockHash == blockHash)
            {
                votes++;
            }
        }

        // Check if quorum is reached
        double voteRatio = (double)votes / sampledNodes.Count;
        return voteRatio >= _quorumThreshold;
    }

    /// <summary>
    /// Update node preference based on query results
    /// </summary>
    public bool UpdatePreference(Node node, string blockHash, bool querySuccess)
    {
        if (node.PreferredBlock == null)
        {
            node.PreferredBlock = new BlockPreference(blockHash);
        }

        if (querySuccess && node.PreferredBlock.BlockHash == blockHash)
        {
            node.PreferredBlock.Confidence++;
            node.PreferredBlock.ConsecutiveSuccesses++;
        }
        else if (!querySuccess)
        {
            node.PreferredBlock.ConsecutiveSuccesses = 0;
        }

        // Decision reached when consecutive successes exceed threshold
        return node.PreferredBlock.ConsecutiveSuccesses >= _decisionThreshold;
    }

    private List<Node> SampleNodes(List<Node> allNodes, Node excludeNode, int count)
    {
        var availableNodes = allNodes.Where(n => n.Id != excludeNode.Id).ToList();
        var sampled = new List<Node>();
        
        while (sampled.Count < Math.Min(count, availableNodes.Count))
        {
            var index = _random.Next(availableNodes.Count);
            if (!sampled.Contains(availableNodes[index]))
            {
                sampled.Add(availableNodes[index]);
            }
        }

        return sampled;
    }
}
