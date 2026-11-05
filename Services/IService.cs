using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProgressReport.DTO;

namespace ProgressReport.Services
{
    public interface IService
    {
        Task<IEnumerable<ServiceDto>> GetAll();
        Task<ServiceDto> GetById(long id);
        Task<ServiceDto> Create(RequestServiceDto dto);
        Task<ServiceDto> Update(long id, RequestServiceDto dto);
        Task<bool> Delete(long id);
    }
}