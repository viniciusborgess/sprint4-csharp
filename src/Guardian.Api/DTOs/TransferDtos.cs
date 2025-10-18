using Guardian.Api.Domain;

namespace Guardian.Api.DTOs
{
    public record TransferCreateDto(int UserId, int PlatformId, decimal Amount, TransferStatus Status);
    public record TransferReadDto(int Id, int UserId, int PlatformId, decimal Amount, DateTimeOffset CreatedAt, TransferStatus Status);
}
