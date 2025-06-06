using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD12.Models
{
    [Table("Country")]
    public partial class Country
    {
        public Country()
        {
            CountryTrips = new HashSet<CountryTrip>();
        }

        [Key]
        public int IdCountry { get; set; }

        [Required]
        [StringLength(120)]
        public string Name { get; set; } = null!;

        public virtual ICollection<CountryTrip> CountryTrips { get; set; }
    }
}