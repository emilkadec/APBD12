using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD12.Models
{
    [Table("Country_Trip")]
    public partial class CountryTrip
    {
        [Key]
        public int IdCountry { get; set; }

        [Key]
        public int IdTrip { get; set; }

        [ForeignKey("IdCountry")]
        public virtual Country Country { get; set; } = null!;

        [ForeignKey("IdTrip")]
        public virtual Trip Trip { get; set; } = null!;
    }
}