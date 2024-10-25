using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class MsCustomer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Customer_id { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Email { get; set; } = string.Empty;
        public int Password { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "VARCHAR(255)")]
        public string Phone_number { get; set; } = string.Empty;

        [Column(TypeName = "VARCHAR(255)")]
        public string Address { get; set; } = string.Empty;
        
        [Column(TypeName = "VARCHAR(255)")]
        public string Driver_license_number { get; set; } = string.Empty;
    }
}
