namespace Guardian.Api.DTOs
{
    public record UserCreateDto(string Name, string Email);
    public record UserReadDto(int Id, string Name, string Email);
}
