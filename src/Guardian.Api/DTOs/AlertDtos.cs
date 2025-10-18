namespace Guardian.Api.DTOs
{
    public record AlertCreateDto(int UserId, int? TransferId, string Message);
    public record AlertReadDto(int Id, int UserId, int? TransferId, string Message, DateTimeOffset CreatedAt);
}
