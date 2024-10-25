using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class MsEmployee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Employee_id { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "VARCHAR(255)")]
        public string Position { get; set; } = string.Empty;

        [Column(TypeName = "VARCHAR(255)")]
        public string Email { get; set; } = string.Empty;

        [Column(TypeName = "VARCHAR(255)")]
        public string Phone_number { get; set; } = string.Empty;
    }
}
