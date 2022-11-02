using System;
using System.Collections.Generic;
using VouwwandImages.Attributes;
using VouwwandImages.Extensions;

namespace VouwwandImages.Database
{
    public class EnumUpdater
    {
        private readonly VouwwandenDbContext _dbContext;

        public EnumUpdater(VouwwandenDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update()
        {
            UpdateProductTypes();
            UpdateBrands();
            UpdateProfiles();
        }

        private void UpdateProductTypes()
        {
            foreach (ProductTypeEntity entity in GetProductTypes())
            {
                var existing = _dbContext.ProductTypes.Find(entity.Id);
                if (existing == null)
                {
                    _dbContext.ProductTypes.Add(entity);
                    _dbContext.SaveChanges();
                }
            }
        }

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

        private void UpdateBrands()
        {
            foreach (BrandEntity entity in GetBrands())
            {
                var existing = _dbContext.Brands.Find(entity.Id);
                if (existing == null)
                {
                    _dbContext.Brands.Add(entity);
                    _dbContext.SaveChanges();
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

        private void UpdateProfiles()
        {
            foreach (ProfileEntity entity in GetProfiles())
            {
                var existing = _dbContext.Profiles.Find(entity.Id);
                if (existing == null)
                {
                    _dbContext.Profiles.Add(entity);
                    _dbContext.SaveChanges();
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

                BrandEntity? brand = _dbContext.Brands.Find((int)brandAttribute.Brand);
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
    }
}
