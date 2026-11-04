using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProgressReport.data;
using ProgressReport.DTO;
using ProgressReport.mappers;
using ProgressReport.models;

namespace ProgressReport.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        private readonly IMapper _mapper;

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

            return UserMapper.ToDto(user);
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            return await _context.Users
                .AsNoTracking()
                .Select(u => UserMapper.ToDto(u))
                .ToListAsync();
        }

        public async Task<UserDto?> GetById(long id)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return null;

            return UserMapper.ToDto(user);
        }
    }
}
