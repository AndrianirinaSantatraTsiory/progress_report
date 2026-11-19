using ProgressReport.DTO;
using ProgressReport.models;

namespace ProgressReport.mappers
{
    public static class UserMapper
    {
        public static UserDto ToDto(User user)
        {
            if (user == null) return null!;

            return new UserDto(
                user.Id,
                user.FullName,
                user.Email,
                user.Role.ToString(),
                user.DirectionId
            );
        }   
    }
}