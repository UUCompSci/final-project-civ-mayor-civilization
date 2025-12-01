using Microsoft.EntityFrameworkCore;

public class WorldDbContext : DbContext
{
    public DbSet<Animal> Animals { get; set; }
    public DbSet<Plant> Plants { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=world.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animal>().ToTable("Animals");
        modelBuilder.Entity<Plant>().ToTable("Plants");
    }
}