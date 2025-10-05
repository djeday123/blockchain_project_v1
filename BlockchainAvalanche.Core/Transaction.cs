using System.Security.Cryptography;
using System.Text;

namespace BlockchainAvalanche.Core;

public class Transaction
{
    public string Id { get; private set; }
    public string Sender { get; set; }
    public string Recipient { get; set; }
    public decimal Amount { get; set; }
    public DateTime Timestamp { get; set; }
    public string Signature { get; set; }

    public Transaction(string sender, string recipient, decimal amount)
    {
        Sender = sender;
        Recipient = recipient;
        Amount = amount;
        Timestamp = DateTime.UtcNow;
        Id = GenerateId();
        Signature = string.Empty;
    }

    private string GenerateId()
    {
        var input = $"{Sender}{Recipient}{Amount}{Timestamp:O}";
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(hashBytes).ToLower();
    }

    public override string ToString()
    {
        return $"Transaction[{Id[..8]}...]: {Sender} -> {Recipient}: {Amount}";
    }
}
