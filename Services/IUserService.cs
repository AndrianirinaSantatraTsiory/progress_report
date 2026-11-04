using ProgressReport.DTO;

namespace ProgressReport.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto?> GetById(long id);
        Task<UserDto> Create(CreateUserDto dto);
    }
}