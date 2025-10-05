using BlockchainAvalanche.Core;

Console.WriteLine("=== Avalanche-style Blockchain Demo ===\n");

// Create a network with multiple nodes
var network = new Network();
var node1 = network.AddNode(miningDifficulty: 2);
var node2 = network.AddNode(miningDifficulty: 2);
var node3 = network.AddNode(miningDifficulty: 2);

Console.WriteLine($"Network initialized with {network.Nodes.Count} nodes\n");

// Create and broadcast some transactions
Console.WriteLine("Creating transactions...");
var tx1 = new Transaction("Alice", "Bob", 50);
var tx2 = new Transaction("Bob", "Charlie", 25);
var tx3 = new Transaction("Charlie", "Alice", 10);

network.BroadcastTransaction(tx1);
network.BroadcastTransaction(tx2);
network.BroadcastTransaction(tx3);

Console.WriteLine($"Transaction 1: {tx1}");
Console.WriteLine($"Transaction 2: {tx2}");
Console.WriteLine($"Transaction 3: {tx3}\n");

// Mine blocks on different nodes
Console.WriteLine("Mining blocks...");
var block1 = node1.MinePendingTransactions("Miner1");
if (block1 != null)
{
    Console.WriteLine($"Node1 mined: {block1}");
}

var block2 = node2.MinePendingTransactions("Miner2");
if (block2 != null)
{
    Console.WriteLine($"Node2 mined: {block2}\n");
}

// Display network status
network.PrintNetworkStatus();

// Display balances
Console.WriteLine("=== Balances ===");
var addresses = new[] { "Alice", "Bob", "Charlie", "Miner1", "Miner2" };
foreach (var address in addresses)
{
    var balance = node1.GetBalance(address);
    Console.WriteLine($"{address}: {balance}");
}

// Verify chain validity
Console.WriteLine($"\n=== Chain Validation ===");
Console.WriteLine($"Node1 chain valid: {node1.IsChainValid()}");
Console.WriteLine($"Node2 chain valid: {node2.IsChainValid()}");
Console.WriteLine($"Node3 chain valid: {node3.IsChainValid()}");

// Display the blockchain
Console.WriteLine($"\n=== Node1 Blockchain ===");
foreach (var block in node1.Chain)
{
    Console.WriteLine($"\n{block}");
    Console.WriteLine($"  Previous Hash: {block.PreviousHash}");
    Console.WriteLine($"  Timestamp: {block.Timestamp}");
    Console.WriteLine($"  Nonce: {block.Nonce}");
    
    if (block.Transactions.Any())
    {
        Console.WriteLine("  Transactions:");
        foreach (var tx in block.Transactions)
        {
            Console.WriteLine($"    - {tx}");
        }
    }
}

Console.WriteLine("\n=== Demo Complete ===");
