using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD12.Models
{
    [Table("Trip")]
    public partial class Trip
    {
        public Trip()
        {
            ClientTrips = new HashSet<ClientTrip>();
            CountryTrips = new HashSet<CountryTrip>();
        }

        [Key]
        public int IdTrip { get; set; }

        [Required]
        [StringLength(120)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(220)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime DateFrom { get; set; }

        [Required]
        public DateTime DateTo { get; set; }

        [Required]
        public int MaxPeople { get; set; }

        public virtual ICollection<ClientTrip> ClientTrips { get; set; }
        public virtual ICollection<CountryTrip> CountryTrips { get; set; }
    }
}