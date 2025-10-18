namespace Guardian.Api.Domain
{
    public class Alert
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int? TransferId { get; set; }
        public PixTransfer? Transfer { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
