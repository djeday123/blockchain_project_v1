namespace BlockchainAvalanche.Core;

public class SnowballConsensus
{
    private readonly int _sampleSize;
    private readonly int _quorumSize;
    private readonly int _decisionThreshold;
    private readonly Random _random;

    public SnowballConsensus(int sampleSize = 5, int quorumSize = 3, int decisionThreshold = 4)
    {
        _sampleSize = sampleSize;
        _quorumSize = quorumSize;
        _decisionThreshold = decisionThreshold;
        _random = new Random();
    }

    public bool ReachConsensus(Block block, List<Node> allNodes, Node currentNode)
    {
        var consecutiveSuccesses = 0;
        var preference = true;

        for (int round = 0; round < _decisionThreshold; round++)
        {
            var sample = SampleNodes(allNodes, currentNode);
            var votes = 0;

            foreach (var node in sample)
            {
                if (node.ValidateBlock(block))
                {
                    votes++;
                }
            }

            if (votes >= _quorumSize)
            {
                if (preference)
                {
                    consecutiveSuccesses++;
                }
                else
                {
                    preference = true;
                    consecutiveSuccesses = 1;
                }
            }
            else
            {
                if (!preference)
                {
                    consecutiveSuccesses++;
                }
                else
                {
                    preference = false;
                    consecutiveSuccesses = 1;
                }
            }

            if (consecutiveSuccesses >= _decisionThreshold)
            {
                return preference;
            }
        }

        return preference;
    }

    private List<Node> SampleNodes(List<Node> allNodes, Node excludeNode)
    {
        var availableNodes = allNodes.Where(n => n.Id != excludeNode.Id).ToList();
        var sampleSize = Math.Min(_sampleSize, availableNodes.Count);
        
        return availableNodes.OrderBy(x => _random.Next()).Take(sampleSize).ToList();
    }
}
