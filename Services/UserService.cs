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
        public async Task<UserDto> Create(RequestUserDto dto)
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
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id) ?? throw new KeyNotFoundException($"L'utilisateur avec l'ID {id} est introuvable.");
            return UserMapper.ToDto(user);
        }

        public async Task<UserDto?> Update(long id, RequestUserDto dto)
        {
            var user = await _context.Users.FindAsync(id) ?? throw new KeyNotFoundException($"L'utilisateur avec l'ID {id} est introuvable.");
            user.FullName = dto.FullName;
            user.Email = dto.Email;
            user.Role = Enum.Parse<UserRole>(dto.Role, true);
            user.ServiceId = dto.ServiceId;

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                user.Password = _passwordHasher.HashPassword(user, dto.Password);
            }

            await _context.SaveChangesAsync();

            return UserMapper.ToDto(user);
        }

        public async Task<bool> Delete(long id)
        {
            var user = await _context.Users.FindAsync(id) ?? throw new KeyNotFoundException($"L'utilisateur avec l'ID {id} est introuvable.");
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
