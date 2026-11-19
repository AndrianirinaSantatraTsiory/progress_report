using System.ComponentModel.DataAnnotations;

namespace ProgressReport.DTO
{
    public record RequestDirectionDto
    {
        [Required(ErrorMessage = "Le nom du service est obligatoire.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Le nom du service doit contenir entre 2 et 50 caractères.")]
        public string Name { get; init; } = string.Empty;
    }
}