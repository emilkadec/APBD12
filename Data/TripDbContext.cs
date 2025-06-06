using Microsoft.EntityFrameworkCore;
using APBD12.Models;

namespace APBD12.Data
{
    public partial class TripDbContext : DbContext
    {
        public TripDbContext(DbContextOptions<TripDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Trip> Trips { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<ClientTrip> ClientTrips { get; set; } = null!;
        public virtual DbSet<CountryTrip> CountryTrips { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientTrip>(entity =>
            {
                entity.HasKey(e => new { e.IdClient, e.IdTrip });

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientTrips)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_5_Client");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.ClientTrips)
                    .HasForeignKey(d => d.IdTrip)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_5_Trip");
            });

            modelBuilder.Entity<CountryTrip>(entity =>
            {
                entity.HasKey(e => new { e.IdCountry, e.IdTrip });

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryTrips)
                    .HasForeignKey(d => d.IdCountry)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Country_Trip_Country");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.CountryTrips)
                    .HasForeignKey(d => d.IdTrip)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Country_Trip_Trip");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}