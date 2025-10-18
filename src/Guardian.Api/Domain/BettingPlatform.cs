namespace Guardian.Api.Domain
{
    public class BettingPlatform
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? PixKey { get; set; }
        public string? Cnpj { get; set; }
        public string? Website { get; set; }
        public ICollection<PixTransfer> Transfers { get; set; } = new List<PixTransfer>();
    }
}
