using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class MsCarImages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Image_Car_Id { get; set; }

        [ForeignKey("MsCar")]
        public int Car_id { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Image_link { get; set; } = string.Empty;
    }
}
