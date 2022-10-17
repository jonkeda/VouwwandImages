using System.Collections.Generic;
using System.Linq;
using VouwwandImages.Database;
using VouwwandImages.UI;

namespace VouwwandImages.ViewModels
{
    public class StoreDataViewModel : ViewModel
    {
        private readonly VouwwandenDbContext _dbContext;
        private BrandEntity _selectedBrand;
        private ProductType _selectedProductType;

        public StoreDataViewModel(VouwwandenDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<BrandEntity> Brands
        {
            get
            {
                return _dbContext.Brands.ToList();
            }
        }

        public IEnumerable<ProductTypeEntity> ProductTypes
        {
            get
            {
                return _dbContext.ProductTypes.ToList();
            }
        }

        public BrandEntity SelectedBrand
        {
            get { return _selectedBrand; }
            set
            {
                SetProperty(ref _selectedBrand, value);
            }
        }

        public ProductType SelectedProductType
        {
            get { return _selectedProductType; }
            set
            {
                SetProperty(ref _selectedProductType, value);

            }
        }
    }
}
