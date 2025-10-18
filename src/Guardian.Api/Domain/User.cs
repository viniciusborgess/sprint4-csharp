namespace Guardian.Api.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public ICollection<PixTransfer> Transfers { get; set; } = new List<PixTransfer>();
        public ICollection<Alert> Alerts { get; set; } = new List<Alert>();
    }
}
