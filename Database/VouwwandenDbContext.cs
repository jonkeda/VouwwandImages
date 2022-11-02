using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VouwwandImages.Attributes;
using VouwwandImages.Extensions;

namespace VouwwandImages.Database;

public sealed class VouwwandenDbContext : DbContext
{
    #region Contructor
    public VouwwandenDbContext(DbContextOptions<VouwwandenDbContext> options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();

    }
    #endregion

    public DbSet<MeasurementEntity> Measurements { get; set; }
    public DbSet<CalculationEntity> Calculations { get; set; }
    public DbSet<SizeMeasurementEntity> SizeMeasurements { get; set; }

    public DbSet<ProductTypeEntity> ProductTypes { get; set; }
    public DbSet<BrandEntity> Brands { get; set; }

    public DbSet<ProfileEntity> Profiles { get; set; }
    public DbSet<DistributionEntity> Distributions { get; set; }

    public DbSet<GlassEntity> Glazing { get; set; }
    public DbSet<ColorEntity> Colors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseChangeTrackingProxies(false)
            .UseLazyLoadingProxies()
            .UseSqlite("Filename=./Vouwwanden.sqlite");
    }

/*    #region Overridden method
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductTypeEntity>().HasData(GetProductTypes());
        modelBuilder.Entity<BrandEntity>().HasData(GetBrands());
        modelBuilder.Entity<ProfileEntity>().HasData(GetProfiles());
        base.OnModelCreating(modelBuilder);
    }
    #endregion

    #region Private method

    private IEnumerable<ProductTypeEntity> GetProductTypes()
    {
        foreach (ProductType type in Enum.GetValues<ProductType>())
        {
            if (type != ProductType.None)
            {
                yield return new ProductTypeEntity
                {
                    Id = (int)type,
                    Name = type.GetDisplayName(),
                    Type = type
                };
            }
        }
    }

    private IEnumerable<BrandEntity> GetBrands()
    {
        foreach (Brand type in Enum.GetValues<Brand>())
        {
            if (type != Brand.None)
            {
                yield return new BrandEntity
                {
                    Id = (int)type,
                    Name = type.GetDisplayName(),
                    Brand = type
                };
            }
        }
    }
    
    private IEnumerable<ProfileEntity> GetProfiles()
    {
        foreach (Profile type in Enum.GetValues<Profile>())
        {
            if (type == Profile.None)
                continue;

            BrandAttribute? brandAttribute = type.GetAttributeOfType<BrandAttribute>();
            if (brandAttribute == null)
                continue;
            
            BrandEntity? brand = Brands.Find((int)brandAttribute.Brand);
            if (brand == null)
                continue;

            yield return new ProfileEntity
            {
                Id = (int)type,
                Name = type.GetDisplayName(),
                Profile = type,
                Brand = brand
            };
        }
    }

    #endregion*/
}