using AvalancheBlockchain.Core;

Console.WriteLine("=== Avalanche Blockchain Educational Demo ===\n");

// Create a network with 10 nodes
Console.WriteLine("1. Creating network with 10 nodes...");
var network = new AvalancheNetwork(sampleSize: 5, quorumThreshold: 0.6, decisionThreshold: 4);

for (int i = 0; i < 10; i++)
{
    var node = new Node($"Node{i}", $"192.168.1.{i}");
    network.AddNode(node);
}

Console.WriteLine($"   Network created with {network.Nodes.Count} nodes\n");

// Create and mine some blocks
Console.WriteLine("2. Creating transactions and mining blocks...");
var mainNode = network.Nodes[0];

// Add transactions
mainNode.Blockchain.AddTransaction(new Transaction("Alice", "Bob", 50));
mainNode.Blockchain.AddTransaction(new Transaction("Bob", "Charlie", 25));

// Mine block 1
Console.WriteLine("   Mining block 1...");
var block1 = mainNode.Blockchain.MinePendingTransactions("Miner1");
Console.WriteLine($"   ✓ Block mined: {block1}");

// Propagate through network using Avalanche consensus
Console.WriteLine("\n3. Propagating block through network using Snowball consensus...");
bool consensus1 = network.PropagateBlock(block1);
Console.WriteLine($"   Consensus reached: {consensus1}");

network.PrintNetworkStatus();

// Add more transactions and mine block 2
Console.WriteLine("\n4. Creating more transactions and mining block 2...");
mainNode.Blockchain.AddTransaction(new Transaction("Charlie", "Dave", 10));
mainNode.Blockchain.AddTransaction(new Transaction("Dave", "Eve", 5));

Console.WriteLine("   Mining block 2...");
var block2 = mainNode.Blockchain.MinePendingTransactions("Miner2");
Console.WriteLine($"   ✓ Block mined: {block2}");

// Propagate block 2
Console.WriteLine("\n5. Propagating block 2 through network...");
bool consensus2 = network.PropagateBlock(block2);
Console.WriteLine($"   Consensus reached: {consensus2}");

network.PrintNetworkStatus();

// Validate blockchain
Console.WriteLine("\n6. Validating blockchain integrity...");
bool isValid = mainNode.Blockchain.IsChainValid();
Console.WriteLine($"   Blockchain valid: {isValid}");

// Display balances
Console.WriteLine("\n7. Account balances:");
var addresses = new[] { "Alice", "Bob", "Charlie", "Dave", "Eve", "Miner1", "Miner2" };
foreach (var address in addresses)
{
    var balance = mainNode.Blockchain.GetBalance(address);
    Console.WriteLine($"   {address}: {balance}");
}

// Display full blockchain
Console.WriteLine("\n8. Complete blockchain:");
foreach (var block in mainNode.Blockchain.Chain)
{
    Console.WriteLine($"\n   {block}");
    Console.WriteLine($"   Hash: {block.Hash}");
    Console.WriteLine($"   Previous: {block.PreviousHash}");
    Console.WriteLine($"   Transactions:");
    foreach (var tx in block.Transactions)
    {
        Console.WriteLine($"      - {tx}");
    }
}

Console.WriteLine("\n=== Demo Complete ===");
