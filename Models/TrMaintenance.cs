using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class TrMaintenance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Maintenance_id { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Maintenance_date { get; set; } = string.Empty;

        [Column(TypeName = "VARCHAR(255)")]
        public string Decription { get; set; } = string.Empty;
        public int Cost { get; set; }

        [ForeignKey("MsCar")]
        public int Car_id { get; set; }
        [ForeignKey("MsEmployee")]
        public int Employee_id { get; set; }
    }
}
