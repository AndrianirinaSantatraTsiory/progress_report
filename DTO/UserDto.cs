using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgressReport.DTO
{
    public record UserDto
    (
        long Id,
        string FullName,
        string Email,
        string Role,
        long? ServiceId
    );
}