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

        public UserService(AppDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserDto> Create(RequestUserDto dto)
        {
            bool emailExists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
            if (emailExists)
            {
                throw new InvalidOperationException($"Un utilisateur avec l'email '{dto.Email}' existe déjà.");
            }

            // 2. Contrôle de l'existence du ServiceId
            bool serviceExists = await _context.Services.AnyAsync(s => s.Id == dto.ServiceId);
            if (!serviceExists)
            {
                throw new KeyNotFoundException($"Le service n'existe plus");
            }

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
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id) 
                ?? throw new KeyNotFoundException($"L'utilisateur avec l'ID {id} est introuvable.");
            return UserMapper.ToDto(user);
        }

        public async Task<UserDto?> Update(long id, RequestUserDto dto)
        {
            var user = await _context.Users.FindAsync(id) 
                ?? throw new KeyNotFoundException($"L'utilisateur avec l'ID {id} est introuvable.");

            bool emailExists = await _context.Users.AnyAsync(u => u.Email == dto.Email && u.Id != id);
            if (emailExists)
            {
                throw new InvalidOperationException($"Un autre utilisateur utilise déjà l'email '{dto.Email}'.");
            }

            bool serviceExists = await _context.Services.AnyAsync(s => s.Id == dto.ServiceId);
            if (!serviceExists)
            {
                throw new KeyNotFoundException($"Le service n'existe plus");
            }

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
            var user = await _context.Users.FindAsync(id) 
                ?? throw new KeyNotFoundException($"L'utilisateur avec l'ID {id} est introuvable.");
            
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<User?> GetUserEntityByEmail(string email)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}