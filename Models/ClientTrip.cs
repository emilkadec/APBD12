using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD12.Models
{
    [Table("Client_Trip")]
    public partial class ClientTrip
    {
        [Key]
        public int IdClient { get; set; }

        [Key]
        public int IdTrip { get; set; }

        [Required]
        public DateTime RegisteredAt { get; set; }

        public DateTime? PaymentDate { get; set; }

        [ForeignKey("IdClient")]
        public virtual Client Client { get; set; } = null!;

        [ForeignKey("IdTrip")]
        public virtual Trip Trip { get; set; } = null!;
    }
}