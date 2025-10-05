# Avalanche Blockchain Educational Project

An educational implementation of a blockchain based on Avalanche's consensus principles. This project demonstrates the core concepts of the Avalanche protocol, including the Snowball consensus mechanism.

## Overview

This project implements a simplified version of Avalanche's blockchain with the following components:

- **Transaction System**: Create and manage transactions between accounts
- **Block Mining**: Proof-of-work mining with configurable difficulty
- **Snowball Consensus**: Avalanche's probabilistic consensus mechanism
- **Network Simulation**: Multi-node network with consensus propagation
- **Blockchain Validation**: Integrity checking and tamper detection

## Avalanche Consensus Principles

The Avalanche protocol uses a family of consensus mechanisms (Snowflake, Snowball, Snowman) that rely on:

1. **Repeated Random Sampling**: Nodes query random subsets of the network
2. **Transitive Voting**: Nodes update preferences based on network responses
3. **Confidence Thresholds**: Decisions are made when confidence reaches a threshold
4. **Byzantine Fault Tolerance**: The system tolerates malicious nodes

### Snowball Consensus Algorithm

This implementation uses a simplified Snowball consensus:

1. Each node maintains a preference for a block
2. Nodes randomly sample other nodes in the network
3. If a quorum of sampled nodes prefer the same block, confidence increases
4. After consecutive successful queries exceed a threshold, the decision is finalized

## Project Structure

```
AvalancheBlockchain/
├── AvalancheBlockchain.Core/          # Core blockchain library
│   ├── Transaction.cs                  # Transaction data structure
│   ├── Block.cs                        # Block data structure
│   ├── Blockchain.cs                   # Blockchain implementation
│   ├── Node.cs                         # Network node
│   ├── SnowballConsensus.cs           # Avalanche consensus algorithm
│   └── AvalancheNetwork.cs            # Network coordinator
├── AvalancheBlockchain.Demo/          # Demo console application
│   └── Program.cs                      # Example usage
└── AvalancheBlockchain.Tests/         # Unit tests
    ├── TransactionTests.cs
    ├── BlockTests.cs
    ├── BlockchainTests.cs
    └── SnowballConsensusTests.cs
```

## Getting Started

### Prerequisites

- .NET 9.0 SDK or later

### Building the Project

```bash
dotnet build
```

### Running Tests

```bash
dotnet test
```

### Running the Demo

```bash
dotnet run --project AvalancheBlockchain.Demo
```

## How It Works

### 1. Creating a Network

```csharp
var network = new AvalancheNetwork(
    sampleSize: 5,           // Number of nodes to sample per query
    quorumThreshold: 0.6,    // 60% agreement required
    decisionThreshold: 4     // 4 consecutive successes to finalize
);

// Add nodes to the network
for (int i = 0; i < 10; i++)
{
    var node = new Node($"Node{i}", $"192.168.1.{i}");
    network.AddNode(node);
}
```

### 2. Creating Transactions and Mining Blocks

```csharp
var blockchain = new Blockchain();
blockchain.AddTransaction(new Transaction("Alice", "Bob", 50));
var block = blockchain.MinePendingTransactions("Miner");
```

### 3. Propagating Blocks with Consensus

```csharp
bool consensusReached = network.PropagateBlock(block);
```

### 4. Validating the Blockchain

```csharp
bool isValid = blockchain.IsChainValid();
```

## Key Concepts

### Transactions
- Unique ID generated from transaction data
- Timestamp for ordering
- Cryptographic hash for integrity

### Blocks
- Contains multiple transactions
- Links to previous block via hash
- Proof-of-work via nonce adjustment
- Merkle-like transaction verification

### Consensus Parameters

- **Sample Size**: Number of nodes queried per round (default: 5)
- **Quorum Threshold**: Percentage agreement required (default: 60%)
- **Decision Threshold**: Consecutive successful rounds to finalize (default: 10)

### Network Behavior

The Avalanche consensus achieves:
- **High Throughput**: Multiple concurrent consensus rounds
- **Low Latency**: Quick finalization with small sample sizes
- **Scalability**: O(k*log n) communication complexity
- **Robustness**: Tolerates Byzantine failures

## Educational Value

This implementation demonstrates:

1. **Blockchain Fundamentals**: Blocks, chains, hashing, and mining
2. **Consensus Mechanisms**: How distributed systems agree on state
3. **Network Protocols**: Node communication and synchronization
4. **Cryptography**: SHA-256 hashing for integrity
5. **Byzantine Fault Tolerance**: Handling malicious or faulty nodes

## Limitations

This is a simplified educational implementation with:

- No real networking (simulated in-memory)
- Simplified Snowball (not full Avalanche DAG)
- Basic proof-of-work (not optimized)
- No persistent storage
- No cryptographic signatures (simplified)

For production use, see [Avalanche documentation](https://docs.avax.network/).

## References

- [Avalanche Whitepaper](https://www.avalabs.org/whitepapers)
- [Snowball Consensus](https://docs.avax.network/learn/avalanche/avalanche-consensus)
- [Avalanche Platform](https://www.avax.network/)

## License

MIT License - See LICENSE file for details
