using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProgressReport.data;
using ProgressReport.DTO;
using ProgressReport.models;

namespace ProgressReport.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(AppDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }
        public async Task<UserDto> Create(CreateUserDto dto)
        {
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Role = Enum.Parse<UserRole>(dto.Role, true),
                ServiceId = dto.ServiceId
            };

            user.Password = _passwordHasher.HashPassword(user, dto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto(user.Id, user.FullName, user.Email, user.Role.ToString(), user.ServiceId);
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            return await _context.Users
                .AsNoTracking()
                .Select(u => new UserDto(
                    u.Id,
                    u.FullName,
                    u.Email,
                    u.Role.ToString(),
                    u.ServiceId
                ))
                .ToListAsync();
        }

        public async Task<UserDto?> GetById(long id)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return null;

            return new UserDto(user.Id, user.FullName, user.Email, user.Role.ToString(), user.ServiceId);
        }
    }
}
