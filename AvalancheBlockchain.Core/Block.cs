using System.Security.Cryptography;
using System.Text;

namespace AvalancheBlockchain.Core;

public class Block
{
    public int Index { get; set; }
    public DateTime Timestamp { get; set; }
    public List<Transaction> Transactions { get; set; }
    public string PreviousHash { get; set; }
    public string Hash { get; set; }
    public int Nonce { get; set; }

    public Block(int index, List<Transaction> transactions, string previousHash = "")
    {
        Index = index;
        Timestamp = DateTime.UtcNow;
        Transactions = transactions ?? new List<Transaction>();
        PreviousHash = previousHash;
        Nonce = 0;
        Hash = CalculateHash();
    }

    public string CalculateHash()
    {
        var transactionData = string.Join("", Transactions.Select(t => t.Id));
        var rawData = $"{Index}{Timestamp:O}{transactionData}{PreviousHash}{Nonce}";
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));
        return Convert.ToHexString(bytes).ToLower();
    }

    public override string ToString()
    {
        return $"Block #{Index} [{Hash[..8]}] with {Transactions.Count} transaction(s)";
    }
}
