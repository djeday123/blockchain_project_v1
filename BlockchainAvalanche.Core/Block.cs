using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace BlockchainAvalanche.Core;

public class Block
{
    public int Index { get; set; }
    public DateTime Timestamp { get; set; }
    public List<Transaction> Transactions { get; set; }
    public string PreviousHash { get; set; }
    public string Hash { get; private set; }
    public int Nonce { get; set; }

    public Block(int index, List<Transaction> transactions, string previousHash)
    {
        Index = index;
        Timestamp = DateTime.UtcNow;
        Transactions = transactions ?? new List<Transaction>();
        PreviousHash = previousHash ?? string.Empty;
        Nonce = 0;
        Hash = CalculateHash();
    }

    public string CalculateHash()
    {
        var transactionData = JsonSerializer.Serialize(Transactions.Select(t => new { t.Id, t.Sender, t.Recipient, t.Amount }));
        var input = $"{Index}{Timestamp:O}{transactionData}{PreviousHash}{Nonce}";
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(hashBytes).ToLower();
    }

    public void MineBlock(int difficulty)
    {
        var target = new string('0', difficulty);
        while (!Hash.StartsWith(target))
        {
            Nonce++;
            Hash = CalculateHash();
        }
    }

    public override string ToString()
    {
        return $"Block #{Index} [Hash: {Hash[..8]}...] with {Transactions.Count} transaction(s)";
    }
}
