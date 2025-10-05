using System.Security.Cryptography;
using System.Text;

namespace AvalancheBlockchain.Core;

public class Transaction
{
    public string Id { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public decimal Amount { get; set; }
    public DateTime Timestamp { get; set; }
    public string Signature { get; set; }

    public Transaction(string from, string to, decimal amount)
    {
        From = from;
        To = to;
        Amount = amount;
        Timestamp = DateTime.UtcNow;
        Id = CalculateHash();
        Signature = string.Empty;
    }

    public string CalculateHash()
    {
        var rawData = $"{From}{To}{Amount}{Timestamp:O}";
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));
        return Convert.ToHexString(bytes).ToLower();
    }

    public override string ToString()
    {
        return $"Transaction[{Id[..8]}]: {From} -> {To} ({Amount})";
    }
}
