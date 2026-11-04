

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgressReport.models
{
    [Table("users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Column("full_name")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Column("password")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Column("role")]
        public UserRole Role { get; set; } = UserRole.COLLABORATOR;

        [Column("service_id")]
        public long? ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        public Service? Service { get; set; }
    }
}