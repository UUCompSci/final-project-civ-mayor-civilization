using Microsoft.EntityFrameworkCore;
using MyProject.Models;

namespace Final_Project_Civ_Mayor_Civilization.Data
{
    // Database context for storing world state using Entity Framework Core
    public class WorldDbContext : DbContext
    {
        // Table for buildings
        public DbSet<Building> Buildings { get; set; } = null!;

        // Table for resources
        public DbSet<Resource> Resources { get; set; } = null!;

        // Table for terrain features
        public DbSet<TerrainFeature> TerrainFeatures { get; set; } = null!;

        // Table for units
        public DbSet<Unit> Units { get; set; } = null!;

        // Configures the SQLite database connection
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // Use relative path to avoid quoting issues
            options.UseSqlite("Data Source=world.db");
        }

        // Maps C# classes to SQLite table names
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Table-per-hierarchy (TPH) for Buildings
            modelBuilder.Entity<Building>()
                .ToTable("Buildings")
                .HasDiscriminator<string>("BuildingType") // discriminates subclasses
                .HasValue<TownCenter>("TownCenter")
                .HasValue<House>("House");

            // Table-per-hierarchy (TPH) for Units
            modelBuilder.Entity<Unit>()
                .ToTable("Units")
                .HasDiscriminator<string>("UnitType") // discriminates subclasses
                .HasValue<Villager>("Villager")
                .HasValue<Scout>("Scout")
                .HasValue<Barbarian>("Barbarian");

            // Other tables
            modelBuilder.Entity<Resource>().ToTable("Resources");
            modelBuilder.Entity<TerrainFeature>().ToTable("TerrainFeatures");
        }
    }
}



