using DD.AccessService.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace DD.AccessService.Infrastructure.Persistence;

internal class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ApiKeyEntity> ApiKeys { get; set; }
    public DbSet<ClaimEntity> Claims { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //TODO: Move to a configuration class.
        modelBuilder
            .Entity<UserEntity>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<UserEntity>().HasData(new UserEntity
        {
            Id = "ADA96D6D-7214-40BE-BC76-A4741A879D8E",
            Name = "John",
            Surname = "Doe"
        });

        modelBuilder.Entity<ApiKeyEntity>()
            .HasKey(x => x.Id);
        
        modelBuilder.Entity<ApiKeyEntity>()
            .HasIndex(x => x.UserId);
        
        modelBuilder.Entity<ApiKeyEntity>()
            .HasIndex(x => x.Value);
        
        modelBuilder.Entity<ApiKeyEntity>()
            .HasOne<UserEntity>()
            .WithMany(x => x.ApiKeys);

        modelBuilder.Entity<ClaimEntity>()
            .HasKey(x => x.Id);
        
        modelBuilder.Entity<ClaimEntity>()
            .HasOne<ApiKeyEntity>(x => x.ApiKey)
            .WithMany(x => x.Claims);
        
        base.OnModelCreating(modelBuilder);
    }
}