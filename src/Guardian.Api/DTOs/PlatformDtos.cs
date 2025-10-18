namespace Guardian.Api.DTOs
{
    public record PlatformCreateDto(string Name, string? PixKey, string? Cnpj, string? Website);
    public record PlatformReadDto(int Id, string Name, string? PixKey, string? Cnpj, string? Website);
}
