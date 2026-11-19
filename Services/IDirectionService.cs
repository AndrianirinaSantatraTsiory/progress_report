using ProgressReport.DTO;

namespace ProgressReport.Services
{
    public interface IDirectionService
    {
        Task<IEnumerable<DirectionDto>> GetAll();
        Task<DirectionDto> GetById(long id);
        Task<DirectionDto> Create(RequestDirectionDto dto);
        Task<DirectionDto> Update(long id, RequestDirectionDto dto);
        Task<bool> Delete(long id);
    }
}