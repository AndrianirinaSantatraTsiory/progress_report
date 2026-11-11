using ProgressReport.DTO;
using ProgressReport.models;

namespace ProgressReport.mappers
{
    public class ServiceMapper
    {
        public static ServiceDto ToDto(Service service)
        {
            if (service == null) return null!;

            return new ServiceDto(
                service.Id,
                service.Name
            );
        }
    }
}