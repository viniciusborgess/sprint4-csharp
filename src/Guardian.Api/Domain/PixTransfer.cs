namespace Guardian.Api.Domain
{
    public enum TransferStatus { Planned, Completed, Cancelled }

    public class PixTransfer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int PlatformId { get; set; }
        public BettingPlatform? Platform { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public TransferStatus Status { get; set; } = TransferStatus.Planned;
    }
}
