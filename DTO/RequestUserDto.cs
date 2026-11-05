namespace ProgressReport.DTO
{
    public record RequestUserDto
    (
        string FullName,
        string Email,
        string Password,
        string Role,
        long? ServiceId
    );
}