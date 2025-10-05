namespace AvalancheBlockchain.Core;

/// <summary>
/// Represents a node in the Avalanche network
/// </summary>
public class Node
{
    public string Id { get; set; }
    public string Address { get; set; }
    public BlockPreference? PreferredBlock { get; set; }
    public Blockchain Blockchain { get; set; }

    public Node(string id, string address)
    {
        Id = id;
        Address = address;
        Blockchain = new Blockchain();
    }

    public void ReceiveBlock(Block block)
    {
        // Initialize preference if not set
        if (PreferredBlock == null)
        {
            PreferredBlock = new BlockPreference(block.Hash);
        }
    }

    public override string ToString()
    {
        return $"Node[{Id}] at {Address}";
    }
}
