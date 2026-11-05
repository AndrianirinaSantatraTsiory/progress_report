using System.ComponentModel.DataAnnotations;

namespace ProgressReport.DTO
{
    public record RequestUserDto
    {
        [Required(ErrorMessage = "Le nom complet est obligatoire.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Le nom doit contenir entre 2 et 100 caractères.")]
        public string FullName { get; init; } = string.Empty;

        [Required(ErrorMessage = "L'email est obligatoire.")]
        [EmailAddress(ErrorMessage = "Le format de l'email est invalide.")]
        public string Email { get; init; } = string.Empty;

        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [MinLength(6, ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères.")]
        public string Password { get; init; } = string.Empty;

        [Required(ErrorMessage = "Le rôle est obligatoire.")]
        public string Role { get; init; } = string.Empty;

        public long? ServiceId { get; init; }
    }
}