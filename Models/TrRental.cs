using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class TrRental
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Rental_id { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Rental_Date { get; set; } = string.Empty;

        [Column(TypeName = "VARCHAR(255)")]
        public string Return_Date { get; set; } = string.Empty;
        public int Total_price { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Payment_status { get; set; } = string.Empty;

        [ForeignKey("MsCustomer")]
        public int Customer_id { get; set; }
        [ForeignKey("MsCar")]
        public int Car_id { get; set; }
    }
}
