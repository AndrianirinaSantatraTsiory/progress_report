using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProgressReport.models
{
    [Table("services")]
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        public ICollection<User> Users { get; set; } = new List<User>(); 
    }
}