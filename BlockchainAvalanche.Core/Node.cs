namespace BlockchainAvalanche.Core;

public class Node
{
    public string Id { get; private set; }
    public List<Block> Chain { get; private set; }
    public List<Transaction> PendingTransactions { get; private set; }
    private readonly int _miningDifficulty;
    private readonly SnowballConsensus _consensus;

    public Node(string id, int miningDifficulty = 2)
    {
        Id = id;
        Chain = new List<Block>();
        PendingTransactions = new List<Transaction>();
        _miningDifficulty = miningDifficulty;
        _consensus = new SnowballConsensus();
        
        CreateGenesisBlock();
    }

    private void CreateGenesisBlock()
    {
        var genesisBlock = new Block(0, new List<Transaction>(), "0");
        genesisBlock.MineBlock(_miningDifficulty);
        Chain.Add(genesisBlock);
    }

    public void AddTransaction(Transaction transaction)
    {
        if (string.IsNullOrEmpty(transaction.Sender) || string.IsNullOrEmpty(transaction.Recipient))
        {
            throw new ArgumentException("Transaction must include sender and recipient");
        }

        PendingTransactions.Add(transaction);
    }

    public Block? MinePendingTransactions(string minerAddress)
    {
        if (!PendingTransactions.Any())
        {
            return null;
        }

        var block = new Block(Chain.Count, new List<Transaction>(PendingTransactions), GetLatestBlock().Hash);
        block.MineBlock(_miningDifficulty);

        Chain.Add(block);
        PendingTransactions.Clear();
        
        PendingTransactions.Add(new Transaction("system", minerAddress, 1));

        return block;
    }

    public bool ValidateBlock(Block block)
    {
        if (block.Index != Chain.Count)
        {
            return false;
        }

        if (block.PreviousHash != GetLatestBlock().Hash)
        {
            return false;
        }

        if (block.Hash != block.CalculateHash())
        {
            return false;
        }

        return true;
    }

    public bool ProposeBlock(Block block, List<Node> network)
    {
        if (!ValidateBlock(block))
        {
            return false;
        }

        var consensus = _consensus.ReachConsensus(block, network, this);
        
        if (consensus)
        {
            Chain.Add(block);
            PendingTransactions.Clear();
        }

        return consensus;
    }

    public bool IsChainValid()
    {
        for (int i = 1; i < Chain.Count; i++)
        {
            var currentBlock = Chain[i];
            var previousBlock = Chain[i - 1];

            if (currentBlock.Hash != currentBlock.CalculateHash())
            {
                return false;
            }

            if (currentBlock.PreviousHash != previousBlock.Hash)
            {
                return false;
            }
        }

        return true;
    }

    public Block GetLatestBlock()
    {
        return Chain[^1];
    }

    public decimal GetBalance(string address)
    {
        decimal balance = 0;

        foreach (var block in Chain)
        {
            foreach (var transaction in block.Transactions)
            {
                if (transaction.Sender == address)
                {
                    balance -= transaction.Amount;
                }

                if (transaction.Recipient == address)
                {
                    balance += transaction.Amount;
                }
            }
        }

        return balance;
    }
}
