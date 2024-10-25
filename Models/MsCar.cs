using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class MsCar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Car_id { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "VARCHAR(255)")]
        public string Model { get; set; } = string.Empty;

        public int Year { get; set; } 

        [Column(TypeName = "VARCHAR(255)")]
        public string License_Plate { get; set; } = string.Empty;

        public int Number_of_car_seats { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Transmission { get; set; } = string.Empty;
        
        public int Price_per_day { get; set; }
        
        [Column(TypeName = "VARCHAR(255)")]
        public string Status { get; set; } = string.Empty;
    }
}
