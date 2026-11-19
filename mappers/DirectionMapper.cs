using ProgressReport.DTO;
using ProgressReport.models;

namespace ProgressReport.mappers
{
    public class DirectionMapper
    {
        public static DirectionDto ToDto(Direction service)
        {
            if (service == null) return null!;

            return new DirectionDto(
                service.Id,
                service.Name
            );
        }
    }
}