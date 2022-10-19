using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using VouwwandImages.Database;
using VouwwandImages.UI;

namespace VouwwandImages.ViewModels
{
    public class DataViewModel: ViewModel
    {
        private readonly VouwwandenDbContext _dbContext;
        private BrandEntity _selectedBrand;
        private ProductType _selectedProductType;

        public DataViewModel(VouwwandenDbContext dbContext)
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

        public IEnumerable<MeasurementEntity> Measurements
        {
            get
            {
                var query = _dbContext.Measurements;

                return query.ToList();
            }
        }

        public BrandEntity SelectedBrand
        {
            get { return _selectedBrand; }
            set
            {
                if (SetProperty(ref _selectedBrand, value))
                {
                    NotifyPropertyChanged(nameof(Measurements));
                }
            }
        }

        public ProductType SelectedProductType
        {
            get { return _selectedProductType; }
            set { 
                if (SetProperty(ref _selectedProductType, value))
                {
                    NotifyPropertyChanged(nameof(Measurements));
                }
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new TargetCommand(Refresh);
            }
        }

        private void Refresh()
        {
            NotifyPropertyChanged(nameof(Brands));
            NotifyPropertyChanged(nameof(ProductTypes));
            NotifyPropertyChanged(nameof(Measurements));
        }
    }
}
