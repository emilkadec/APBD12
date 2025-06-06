using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD12.Models
{
    [Table("Client")]
    public partial class Client
    {
        public Client()
        {
            ClientTrips = new HashSet<ClientTrip>();
        }

        [Key]
        public int IdClient { get; set; }

        [Required]
        [StringLength(120)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(120)]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(120)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(120)]
        public string Telephone { get; set; } = null!;

        [Required]
        [StringLength(120)]
        public string Pesel { get; set; } = null!;

        public virtual ICollection<ClientTrip> ClientTrips { get; set; }
    }
}