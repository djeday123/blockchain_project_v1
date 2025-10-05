namespace BlockchainAvalanche.Core;

public class Network
{
    public List<Node> Nodes { get; private set; }
    private int _nodeCounter;

    public Network()
    {
        Nodes = new List<Node>();
        _nodeCounter = 0;
    }

    public Node AddNode(int miningDifficulty = 2)
    {
        var node = new Node($"Node-{_nodeCounter++}", miningDifficulty);
        Nodes.Add(node);
        return node;
    }

    public void BroadcastTransaction(Transaction transaction)
    {
        foreach (var node in Nodes)
        {
            node.AddTransaction(transaction);
        }
    }

    public void SyncChains()
    {
        if (Nodes.Count == 0) return;

        var longestChain = Nodes.OrderByDescending(n => n.Chain.Count).First().Chain;
        
        foreach (var node in Nodes)
        {
            if (node.Chain.Count < longestChain.Count && node.IsChainValid())
            {
                node.Chain.Clear();
                foreach (var block in longestChain)
                {
                    node.Chain.Add(block);
                }
            }
        }
    }

    public void PrintNetworkStatus()
    {
        Console.WriteLine("\n=== Network Status ===");
        foreach (var node in Nodes)
        {
            Console.WriteLine($"\n{node.Id}:");
            Console.WriteLine($"  Chain Length: {node.Chain.Count}");
            Console.WriteLine($"  Valid: {node.IsChainValid()}");
            Console.WriteLine($"  Pending Transactions: {node.PendingTransactions.Count}");
        }
        Console.WriteLine("=====================\n");
    }
}
