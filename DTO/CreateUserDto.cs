namespace ProgressReport.DTO
{
    public record CreateUserDto
    (
        string FullName,
        string Email,
        string Password,
        string Role,
        long? ServiceId
    );
}