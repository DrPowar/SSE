using SolarSystemEncyclopedia.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace SolarSystemEncyclopedia.Data
{
    public class SolarSystemContext : DbContext
    {
        public DbSet<CosmicObject> CosmicObject { get; set; } = default!;
        public DbSet<Planet> Planet { get; set; } = default!;
        public DbSet<Star> Star { get; set; } = default!;
        public DbSet<Moon> Moon { get; set; } = default!;
        public SolarSystemContext(DbContextOptions<SolarSystemContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Planet>()
                .HasMany(p => p.Moons)
                .WithOne(m => m.MainPlanet);

            modelBuilder.Entity<Star>()
                .HasMany(s => s.Planets)
                .WithOne(p => p.MainStar);
        }

    }
}