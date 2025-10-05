namespace AvalancheBlockchain.Core;

/// <summary>
/// Blockchain implementation using Avalanche consensus principles
/// </summary>
public class Blockchain
{
    public List<Block> Chain { get; private set; }
    public List<Transaction> PendingTransactions { get; private set; }
    public int Difficulty { get; set; }

    public Blockchain()
    {
        Chain = new List<Block>();
        PendingTransactions = new List<Transaction>();
        Difficulty = 2;
        
        // Create genesis block
        CreateGenesisBlock();
    }

    private void CreateGenesisBlock()
    {
        var genesisTransactions = new List<Transaction>
        {
            new Transaction("system", "genesis", 0)
        };
        var genesisBlock = new Block(0, genesisTransactions, "0");
        Chain.Add(genesisBlock);
    }

    public Block GetLatestBlock()
    {
        return Chain[^1];
    }

    public void AddTransaction(Transaction transaction)
    {
        if (string.IsNullOrEmpty(transaction.From) || string.IsNullOrEmpty(transaction.To))
        {
            throw new ArgumentException("Transaction must include from and to address");
        }

        PendingTransactions.Add(transaction);
    }

    public Block MinePendingTransactions(string minerAddress)
    {
        var block = new Block(Chain.Count, new List<Transaction>(PendingTransactions), GetLatestBlock().Hash);
        
        // Simple proof of work (for demonstration)
        while (!block.Hash.StartsWith(new string('0', Difficulty)))
        {
            block.Nonce++;
            block.Hash = block.CalculateHash();
        }

        Chain.Add(block);
        
        // Reward miner
        PendingTransactions = new List<Transaction>
        {
            new Transaction("system", minerAddress, 1)
        };

        return block;
    }

    public bool IsChainValid()
    {
        for (int i = 1; i < Chain.Count; i++)
        {
            var currentBlock = Chain[i];
            var previousBlock = Chain[i - 1];

            // Verify transaction integrity
            foreach (var tx in currentBlock.Transactions)
            {
                if (tx.Id != tx.CalculateHash())
                {
                    return false;
                }
            }

            // Verify hash
            if (currentBlock.Hash != currentBlock.CalculateHash())
            {
                return false;
            }

            // Verify link
            if (currentBlock.PreviousHash != previousBlock.Hash)
            {
                return false;
            }

            // Verify proof of work
            if (!currentBlock.Hash.StartsWith(new string('0', Difficulty)))
            {
                return false;
            }
        }

        return true;
    }

    public decimal GetBalance(string address)
    {
        decimal balance = 0;

        foreach (var block in Chain)
        {
            foreach (var transaction in block.Transactions)
            {
                if (transaction.From == address)
                {
                    balance -= transaction.Amount;
                }

                if (transaction.To == address)
                {
                    balance += transaction.Amount;
                }
            }
        }

        return balance;
    }
}
