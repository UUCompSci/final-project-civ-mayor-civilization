// removed animals and plants and added buildings, units, resources, and terrain features.
using Microsoft.EntityFrameworkCore;

public class WorldDbContext : DbContext
{
    public DbSet<Building> Buildings { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<TerrainFeature> TerrainFeatures { get; set; }
    public DbSet<Unit> Units { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=world.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Building>().ToTable("Buildings");
        modelBuilder.Entity<Resource>().ToTable("Resources");
        modelBuilder.Entity<TerrainFeature>().ToTable("TerrainFeatures");
        modelBuilder.Entity<Unit>().ToTable("Units");
    }
}
