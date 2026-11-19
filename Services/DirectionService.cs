using Microsoft.EntityFrameworkCore;
using ProgressReport.data;
using ProgressReport.DTO;
using ProgressReport.mappers;
using ProgressReport.models;

namespace ProgressReport.Services
{
    public class DirectionService : IDirectionService
    {
        private readonly AppDbContext _context;

        public DirectionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DirectionDto> Create(RequestDirectionDto dto)
        {
            bool nameExists = await _context.Services.AnyAsync(s => s.Name == dto.Name);
            if (nameExists)
            {
                throw new InvalidOperationException($"Un service avec le nom '{dto.Name}' existe déjà.");
            }

            var service = new Direction
            {
                Name = dto.Name
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return DirectionMapper.ToDto(service);
        }

        public async Task<IEnumerable<DirectionDto>> GetAll()
        {
            return await _context.Services
                .AsNoTracking()
                .Select(s => DirectionMapper.ToDto(s))
                .ToListAsync();
        }

        public async Task<DirectionDto> GetById(long id)
        {
            var service = await _context.Services
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id) 
                ?? throw new KeyNotFoundException($"Le service avec l'ID {id} est introuvable.");

            return DirectionMapper.ToDto(service);
        }

        public async Task<DirectionDto> Update(long id, RequestDirectionDto dto)
        {
            var service = await _context.Services.FindAsync(id) 
                ?? throw new KeyNotFoundException($"Le service avec l'ID {id} est introuvable.");

            bool nameExists = await _context.Services.AnyAsync(s => s.Name == dto.Name && s.Id != id);
            if (nameExists)
            {
                throw new InvalidOperationException($"Un autre service utilise déjà le nom '{dto.Name}'.");
            }

            service.Name = dto.Name;

            await _context.SaveChangesAsync();

            return DirectionMapper.ToDto(service);
        }

        public async Task<bool> Delete(long id)
        {
            var service = await _context.Services.FindAsync(id) 
                ?? throw new KeyNotFoundException($"Le service avec l'ID {id} est introuvable.");

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}