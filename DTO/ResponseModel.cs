using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgressReport.DTO
{
    public record ResponseModel<T>
    (
        string Message,
        T? Data   
    );
}