# Blockchain Project v1 - Avalanche Consensus Implementation

An educational project demonstrating blockchain principles based on the Avalanche consensus protocol. This implementation provides a simplified version of the Snowball family of consensus protocols used in the Avalanche network.

## Overview

This project implements a basic blockchain with the following features:

- **Block and Transaction Structure**: Traditional blockchain blocks with SHA-256 hashing
- **Proof of Work Mining**: Configurable difficulty mining for block validation
- **Avalanche Consensus**: Simplified implementation of the Snowball consensus protocol
- **Multi-Node Network**: Simulation of a network with multiple validator nodes
- **Transaction Ledger**: Complete transaction history and balance tracking

## Key Concepts from Avalanche

The Avalanche consensus protocol uses a novel approach based on repeated random sampling:

1. **Snowball Consensus**: Nodes repeatedly sample other nodes to reach agreement on transactions
2. **Byzantine Fault Tolerance**: The protocol can tolerate malicious actors in the network
3. **High Throughput**: Consensus can be reached quickly with minimal communication
4. **Decentralized Decision Making**: No single leader; all nodes participate in consensus

## Project Structure

```
BlockchainAvalanche/
├── BlockchainAvalanche.Core/          # Core blockchain implementation
│   ├── Block.cs                        # Block structure and hashing
│   ├── Transaction.cs                  # Transaction model
│   ├── Node.cs                         # Blockchain node implementation
│   ├── SnowballConsensus.cs           # Avalanche consensus protocol
│   └── Network.cs                      # Network simulation
├── BlockchainAvalanche.Demo/          # Demo console application
│   └── Program.cs                      # Example usage
├── BlockchainAvalanche.Tests/         # Unit tests
│   ├── BlockTests.cs
│   ├── TransactionTests.cs
│   ├── NodeTests.cs
│   └── NetworkTests.cs
└── BlockchainAvalanche.sln            # Solution file
```

## Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

### Building the Project

```bash
# Clone the repository
git clone https://github.com/djeday123/blockchain_project_v1.git
cd blockchain_project_v1

# Build the solution
dotnet build

# Run tests
dotnet test

# Run the demo
dotnet run --project BlockchainAvalanche.Demo/BlockchainAvalanche.Demo.csproj
```

## Usage Examples

### Creating a Simple Blockchain

```csharp
using BlockchainAvalanche.Core;

// Create a network
var network = new Network();
var node = network.AddNode(miningDifficulty: 2);

// Add transactions
node.AddTransaction(new Transaction("Alice", "Bob", 50));
node.AddTransaction(new Transaction("Bob", "Charlie", 25));

// Mine pending transactions
var block = node.MinePendingTransactions("MinerAddress");

// Validate the chain
bool isValid = node.IsChainValid();
```

### Working with Multiple Nodes

```csharp
// Create network with multiple nodes
var network = new Network();
var node1 = network.AddNode();
var node2 = network.AddNode();
var node3 = network.AddNode();

// Broadcast transaction to all nodes
var transaction = new Transaction("Alice", "Bob", 100);
network.BroadcastTransaction(transaction);

// Each node can mine independently
node1.MinePendingTransactions("Miner1");
node2.MinePendingTransactions("Miner2");

// Check network status
network.PrintNetworkStatus();
```

## Core Components

### Block
Represents a block in the blockchain with:
- Index (position in chain)
- Timestamp
- List of transactions
- Previous block hash
- Current block hash
- Nonce (for proof of work)

### Transaction
Represents a value transfer with:
- Unique ID (SHA-256 hash)
- Sender address
- Recipient address
- Amount
- Timestamp

### Node
A participant in the blockchain network that can:
- Maintain a copy of the blockchain
- Add and validate transactions
- Mine new blocks
- Participate in consensus
- Calculate account balances

### SnowballConsensus
Implements the Avalanche consensus protocol:
- Random sampling of network nodes
- Repeated voting rounds
- Quorum-based decision making
- Confidence building through consecutive successes

### Network
Simulates a blockchain network:
- Manages multiple nodes
- Broadcasts transactions
- Synchronizes chains
- Displays network status

## Avalanche Consensus Parameters

The Snowball consensus implementation uses these configurable parameters:

- **Sample Size (k)**: Number of nodes to query per round (default: 5)
- **Quorum Size (α)**: Minimum positive responses needed (default: 3)
- **Decision Threshold (β)**: Consecutive successes required for finality (default: 4)

## Testing

The project includes comprehensive unit tests covering:

- Transaction creation and validation
- Block hashing and mining
- Chain validation
- Balance calculations
- Network operations

Run tests with:
```bash
dotnet test --logger "console;verbosity=normal"
```

## Educational Goals

This project is designed to teach:

1. **Blockchain Fundamentals**: Understanding blocks, chains, and transactions
2. **Consensus Mechanisms**: How distributed systems reach agreement
3. **Avalanche Protocol**: The unique approach of repeated random sampling
4. **Proof of Work**: Basic mining and hash-based security
5. **Distributed Systems**: Multi-node networks and synchronization

## Limitations

This is a simplified educational implementation. It does not include:

- Cryptographic signatures for transactions
- Network communication (runs in-memory)
- DAG (Directed Acyclic Graph) structure used in production Avalanche
- Subnets or multiple chains (X-Chain, P-Chain, C-Chain)
- Economic incentives or gas fees
- Advanced Byzantine fault tolerance

## Further Learning

To learn more about Avalanche and blockchain technology:

- [Avalanche Whitepaper](https://www.avalabs.org/whitepapers)
- [Avalanche Documentation](https://docs.avax.network/)
- [Blockchain Basics](https://en.wikipedia.org/wiki/Blockchain)
- [Consensus Protocols](https://en.wikipedia.org/wiki/Consensus_(computer_science))

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contributing

This is an educational project. Feel free to fork, experiment, and learn!

## Acknowledgments

- Inspired by the Avalanche consensus protocol developed by Team Rocket (Ava Labs)
- Based on the Snowflake, Snowball, and Avalanche family of consensus protocols
