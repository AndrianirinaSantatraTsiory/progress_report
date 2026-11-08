using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProgressReport.DTO;

namespace ProgressReport.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Login(LoginDto dto);
    }
}