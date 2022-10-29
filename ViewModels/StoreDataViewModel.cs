using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using VouwwandImages.Database;
using VouwwandImages.UI;

namespace VouwwandImages.ViewModels
{
    public class StoreDataViewModel : ViewModel
    {
        #region Fields

        private readonly VouwwandenDbContext _dbContext;

        private BrandEntity _selectedBrand;

        private ProductTypeEntity _selectedProductType;

        private ProfileEntity _selectedProfile;
        private string? _newProfileName;

        private DistributionEntity _selectedDistribution;
        private string? _newDistributionName;

        private GlassEntity _selectedGlass;
        private string? _newGlassName;
        private string? _newGlassCode;

        private ColorEntity _selectedRalInside;
        private string? _newRalInsideName;
        private string? _newRalInsideCode;

        private ColorEntity _selectedRalOutside;
        private string? _newRalOutsideName;
        private string? _newRalOutsideCode;

        private ColorEntity _selectedRalSashInside;
        private string? _newRalSashInsideName;
        private string? _newRalSashInsideCode;

        private ColorEntity _selectedRalSashOutside;
        private string? _newRalSashOutsideName;
        private string? _newRalSashOutsideCode;

        private double _minimumWidth;
        private double _maximumWidth;
        private double _minimumHeight;
        private double _maximumHeight;

        #endregion

        #region Constructor

        public StoreDataViewModel(VouwwandenDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Brands

        public IEnumerable<BrandEntity> Brands
        {
            get
            {
                return _dbContext.Brands.ToList();
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

        #endregion

        #region ProductTypes

        public IEnumerable<ProductTypeEntity> ProductTypes
        {
            get
            {
                return _dbContext.ProductTypes.ToList();
            }
        }
        public ProductTypeEntity SelectedProductType
        {
            get { return _selectedProductType; }
            set
            {
                SetProperty(ref _selectedProductType, value);

            }
        }

        #endregion

        #region Profiles

        public IEnumerable<ProfileEntity> Profiles
        {
            get
            {
                return _dbContext.Profiles.ToList();
            }
        }
        public ProfileEntity SelectedProfile
        {
            get { return _selectedProfile; }
            set
            {
                SetProperty(ref _selectedProfile, value);

            }
        }
        public string? NewProfileName
        {
            get { return _newProfileName; }
            set { SetProperty(ref _newProfileName, value); }
        }

        #endregion

        #region Distributions

        public IEnumerable<DistributionEntity> Distributions
        {
            get
            {
                return _dbContext.Distributions.ToList();
            }
        }
        public DistributionEntity SelectedDistribution
        {
            get { return _selectedDistribution; }
            set
            {
                SetProperty(ref _selectedDistribution, value);

            }
        }
        public string? NewDistributionName
        {
            get { return _newDistributionName; }
            set { SetProperty(ref _newDistributionName, value); }
        }

        #endregion

        #region Glazing

        public string? FillingType
        {
            get { return _fillingType; }
            set { SetProperty(ref _fillingType, value); }
        }
        public string? GlazingBeads
        {
            get { return _glazingBeads; }
            set { SetProperty(ref _glazingBeads, value); }
        }
        public string? WarmEdgeSpace
        {
            get { return _warmEdgeSpace; }
            set { SetProperty(ref _warmEdgeSpace, value); }
        }

        public IEnumerable<GlassEntity> Glazing
        {
            get
            {
                return _dbContext.Glazing.ToList();
            }
        }
        public GlassEntity SelectedGlass
        {
            get { return _selectedGlass; }
            set
            {
                SetProperty(ref _selectedGlass, value);

            }
        }
        public string? NewGlassCode
        {
            get { return _newGlassCode; }
            set { SetProperty(ref _newGlassCode, value); }
        }
        public string? NewGlassName
        {
            get { return _newGlassName; }
            set { SetProperty(ref _newGlassName, value); }
        }

        #endregion

        #region Color properties

        public IEnumerable<ColorEntity> Colors
        {
            get
            {
                return _dbContext.Colors.ToList();
            }
        }


        public ColorEntity SelectedRalInside
        {
            get { return _selectedRalInside; }
            set
            {
                SetProperty(ref _selectedRalInside, value);

            }
        }
        public string? NewRalInsideCode
        {
            get { return _newRalInsideCode; }
            set { SetProperty(ref _newRalInsideCode, value); }
        }
        public string? NewRalInsideName
        {
            get { return _newRalInsideName; }
            set { SetProperty(ref _newRalInsideName, value); }
        }


        public ColorEntity SelectedRalOutside
        {
            get { return _selectedRalOutside; }
            set
            {
                SetProperty(ref _selectedRalOutside, value);

            }
        }
        public string? NewRalOutsideCode
        {
            get { return _newRalOutsideCode; }
            set { SetProperty(ref _newRalOutsideCode, value); }
        }
        public string? NewRalOutsideName
        {
            get { return _newRalOutsideName; }
            set { SetProperty(ref _newRalOutsideName, value); }
        }


        public ColorEntity SelectedRalSashInside
        {
            get { return _selectedRalSashInside; }
            set
            {
                SetProperty(ref _selectedRalSashInside, value);

            }
        }
        public string? NewRalSashInsideCode
        {
            get { return _newRalSashInsideCode; }
            set { SetProperty(ref _newRalSashInsideCode, value); }
        }
        public string? NewRalSashInsideName
        {
            get { return _newRalSashInsideName; }
            set { SetProperty(ref _newRalSashInsideName, value); }
        }


        public ColorEntity SelectedRalSashOutside
        {
            get { return _selectedRalSashOutside; }
            set
            {
                SetProperty(ref _selectedRalSashOutside, value);

            }
        }
        public string? NewRalSashOutsideCode
        {
            get { return _newRalSashOutsideCode; }
            set { SetProperty(ref _newRalSashOutsideCode, value); }
        }
        public string? NewRalSashOutsideName
        {
            get { return _newRalSashOutsideName; }
            set { SetProperty(ref _newRalSashOutsideName, value); }
        }
        #endregion

        #region Sizes

        public double MinimumWidth
        {
            get { return _minimumWidth; }
            set { SetProperty(ref _minimumWidth, value); }
        }

        public double MaximumWidth
        {
            get { return _maximumWidth; }
            set { SetProperty(ref _maximumWidth, value); }
        }

        public double MinimumHeight
        {
            get { return _minimumHeight; }
            set { SetProperty(ref _minimumHeight, value); }
        }

        public double MaximumHeight
        {
            get { return _maximumHeight; }
            set { SetProperty(ref _maximumHeight, value); }
        }


        private decimal _bars = 1;
        private decimal _pillars = 1;

        public decimal Bars
        {
            get { return _bars; }
            set { SetProperty(ref _bars, value); }
        }

        public decimal Pillars
        {
            get { return _pillars; }
            set { SetProperty(ref _pillars, value); }
        }


        #endregion

        #region Prices

        public double CalculatedPriceWidth
        {
            get { return _calculatedPriceWidth; }
            set { SetProperty(ref _calculatedPriceWidth, value); }
        }

        public double CalculatedPriceHeight
        {
            get { return _calculatedPriceHeight; }
            set { SetProperty(ref _calculatedPriceHeight, value); }
        }

        public double CalculatedPriceSquare
        {
            get { return _calculatedPriceSquare; }
            set { SetProperty(ref _calculatedPriceSquare, value); }
        }

        public double CalculatedPriceBase
        {
            get { return _calculatedPriceBase; }
            set { SetProperty(ref _calculatedPriceBase, value); }
        }

        #endregion

        #region Filter properties

        private double _filterMinimumWidth = 0;
        private double _filterMaximumWidth = 10000;

        private double _filterMinimumHeight = 0;
        private double _filterMaximumHeight = 10000;

        private double _filterMinimumSquare = 0;
        private double _filterMaximumSquare = 100000000;

        private double _filterMinimumLength = 0;
        private double _filterMaximumLength = 100000;
        private string? _fillingType;
        private string? _glazingBeads;
        private string? _warmEdgeSpace;
        private double _calculatedPriceWidth;
        private double _calculatedPriceHeight;
        private double _calculatedPriceSquare;
        private double _calculatedPriceBase;

        public double FilterMinimumWidth
        {
            get { return _filterMinimumWidth; }
            set { SetProperty(ref _filterMinimumWidth, value); }
        }

        public double FilterMaximumWidth
        {
            get { return _filterMaximumWidth; }
            set { SetProperty(ref _filterMaximumWidth, value); }
        }


        public double FilterMinimumHeight
        {
            get { return _filterMinimumHeight; }
            set { SetProperty(ref _filterMinimumHeight, value); }
        }

        public double FilterMaximumHeight
        {
            get { return _filterMaximumHeight; }
            set { SetProperty(ref _filterMaximumHeight, value); }
        }


        public double FilterMinimumSquare
        {
            get { return _filterMinimumSquare; }
            set { SetProperty(ref _filterMinimumSquare, value); }
        }

        public double FilterMaximumSquare
        {
            get { return _filterMaximumSquare; }
            set { SetProperty(ref _filterMaximumSquare, value); }
        }

        public double FilterMinimumLength
        {
            get { return _filterMinimumLength; }
            set { SetProperty(ref _filterMinimumLength, value); }
        }

        public double FilterMaximumLength
        {
            get { return _filterMaximumLength; }
            set { SetProperty(ref _filterMaximumLength, value); }
        }

        #endregion

        #region Commands

        public ICommand SaveMeasurementCommand
        {
            get { return new TargetCommand(SaveMeasurement, true); }
        }

        private void SaveMeasurement()
        {
            try
            {

                MeasurementEntity measurement = new MeasurementEntity
                {
                    Brand = SelectedBrand,
                    ProductType = SelectedProductType,

                    Profile = CreateProfile(),

                    Distribution = CreateDistribution(),

                    Glazing = CreateGlass(),

                    RalInside = CreateColor(NewRalInsideCode, NewRalInsideName, SelectedRalInside),
                    RalOutside = CreateColor(NewRalOutsideCode, NewRalOutsideName, SelectedRalOutside),
                    RalSashInside = CreateColor(NewRalSashInsideCode, NewRalSashInsideName, SelectedRalSashInside),
                    RalSashOutside = CreateColor(NewRalSashOutsideCode, NewRalSashOutsideName, SelectedRalSashOutside),

                    MinimumHeight = MinimumHeight,
                    MaximumHeight = MaximumHeight,
                    MinimumWidth = MinimumWidth,
                    MaximumWidth = MaximumWidth,
                };

                _dbContext.Measurements.Add(measurement);
                _dbContext.SaveChanges();

                // SizeMeasurementEntity

                CalculationEntity calculation = new CalculationEntity
                {
                    Measurement = measurement,

                    FilterMinimumWidth = FilterMinimumWidth,
                    FilterMaximumWidth = FilterMaximumWidth,

                    FilterMinimumHeight = FilterMinimumHeight,
                    FilterMaximumHeight = FilterMaximumHeight,

                    FilterMinimumSquare = FilterMinimumSquare,
                    FilterMaximumSquare = FilterMaximumSquare,

                    FilterMinimumLength = FilterMinimumLength,
                    FilterMaximumLength = FilterMaximumLength,

                    CalculatedPriceBase = CalculatedPriceBase,
                    CalculatedPriceHeight = CalculatedPriceHeight,
                    CalculatedPriceWidth = CalculatedPriceWidth
                };


                calculation.CalculatedPriceBase = CalculatedPriceBase;

                _dbContext.Calculations.Add(calculation);                



                Refresh();
            }
            catch (Exception e)
            {
                string a = e.Message;
            }
        }

        private ProfileEntity CreateProfile()
        {
            if (string.IsNullOrEmpty(NewProfileName))
            {
                return SelectedProfile;
            }
            var existing = _dbContext.Profiles.FirstOrDefault(e => e.Name == NewProfileName);
            if (existing != null)
            {
                return existing;
            }
            var profile = new ProfileEntity
            {
                Brand = SelectedBrand,
                Name = NewProfileName
            };
            _dbContext.Profiles.Add(profile);
            _dbContext.SaveChanges();
            return profile;
        }

        private DistributionEntity CreateDistribution()
        {
            if (string.IsNullOrEmpty(NewDistributionName))
            {
                return SelectedDistribution;
            }
            var existing = _dbContext.Distributions.FirstOrDefault(e => e.Name == NewDistributionName);
            if (existing != null)
            {
                return existing;
            }

            var distribution = new DistributionEntity
            {
                Name = NewDistributionName
            };
            _dbContext.Distributions.Add(distribution);
            _dbContext.SaveChanges();
            return distribution;
        }

        private GlassEntity CreateGlass()
        {
            if (string.IsNullOrEmpty(NewGlassName))
            {
                return SelectedGlass;
            }
            var existing = _dbContext.Glazing.FirstOrDefault(e => e.Name == NewGlassName);
            if (existing != null)
            {
                return existing;
            }

            var glass = new GlassEntity
            {
                Name = NewGlassName
            };
            _dbContext.Glazing.Add(glass);
            _dbContext.SaveChanges();
            return glass;
        }

        private ColorEntity CreateColor(string? newColorName, string? newColorCode, ColorEntity selectedColor)
        {
            if (string.IsNullOrEmpty(newColorName))
            {
                return selectedColor;
            }
            var existing = _dbContext.Colors.FirstOrDefault(e => e.Name == newColorName);
            if (existing != null)
            {
                return existing;
            }

            var color = new ColorEntity
            {
                Code = newColorCode,
                Name = newColorName
            };
            _dbContext.Colors.Add(color);
            _dbContext.SaveChanges();
            return color;
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
            NotifyPropertyChanged(nameof(Colors));
            NotifyPropertyChanged(nameof(Glazing));
            NotifyPropertyChanged(nameof(Profiles));
            NotifyPropertyChanged(nameof(Distributions));
        }

        #endregion
    }
}
