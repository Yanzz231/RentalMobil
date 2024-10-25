using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class LtPayments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Payment_id { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Payment_date { get; set; } = string.Empty;
        public int Amount { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Payment_method { get; set; } = string.Empty;
        [ForeignKey("TrRental")]
        public int Rental_id { get; set; }
    }
}
