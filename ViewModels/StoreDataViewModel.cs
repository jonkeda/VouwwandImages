using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using VouwwandImages.Database;
using VouwwandImages.UI;

namespace VouwwandImages.ViewModels;

public class StoreDataViewModel : ViewModel
{
    #region Fields

    private readonly VouwwandenDbContext _dbContext;

    private BrandEntity? _selectedBrand;

    private ProductTypeEntity? _selectedProductType;

    private ProfileEntity? _selectedProfile;
    private string? _newProfileName;

    private DistributionEntity? _selectedDistribution;
    private string? _newDistributionName;

    private GlassEntity? _selectedGlass;
    private string? _newGlassName;
    private string? _newGlassCode;

    private ColorEntity? _selectedColour;
    private string? _newColourName;
    private string? _newColourCode;

    private ColorEntity? _selectedSashColour;
    private string? _newSashColourName;
    private string? _newSashColourCode;

    private ColorEntity? _selectedRalInnerFrame;
    private string? _newRalInnerFrameName;
    private string? _newRalInnerFrameCode;

    private ColorEntity? _selectedRalOuterFrame;
    private string? _newRalOuterFrameName;
    private string? _newRalOuterFrameCode;

    private ColorEntity? _selectedRalInnerSash;
    private string? _newRalInnerSashName;
    private string? _newRalInnerSashCode;

    private ColorEntity? _selectedRalOuterSash;
    private string? _newRalOuterSashName;
    private string? _newRalOuterSashCode;

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
    public BrandEntity? SelectedBrand
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
    public ProductTypeEntity? SelectedProductType
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
    public ProfileEntity? SelectedProfile
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
    public DistributionEntity? SelectedDistribution
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
    public GlassEntity? SelectedGlass
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


    public ColorEntity? SelectedColour
    {
        get { return _selectedColour; }
        set
        {
            SetProperty(ref _selectedColour, value);

        }
    }
    public string? NewColourCode
    {
        get { return _newColourCode; }
        set { SetProperty(ref _newColourCode, value); }
    }
    public string? NewColourName
    {
        get { return _newColourName; }
        set { SetProperty(ref _newColourName, value); }
    }


    public ColorEntity? SelectedSashColour
    {
        get { return _selectedSashColour; }
        set
        {
            SetProperty(ref _selectedSashColour, value);

        }
    }
    public string? NewSashColourCode
    {
        get { return _newSashColourCode; }
        set { SetProperty(ref _newSashColourCode, value); }
    }
    public string? NewSashColourName
    {
        get { return _newSashColourName; }
        set { SetProperty(ref _newSashColourName, value); }
    }


    public ColorEntity? SelectedRalInnerFrame
    {
        get { return _selectedRalInnerFrame; }
        set
        {
            SetProperty(ref _selectedRalInnerFrame, value);

        }
    }
    public string? NewRalInnerFrameCode
    {
        get { return _newRalInnerFrameCode; }
        set { SetProperty(ref _newRalInnerFrameCode, value); }
    }
    public string? NewRalInnerFrameName
    {
        get { return _newRalInnerFrameName; }
        set { SetProperty(ref _newRalInnerFrameName, value); }
    }


    public ColorEntity? SelectedRalOuterFrame
    {
        get { return _selectedRalOuterFrame; }
        set
        {
            SetProperty(ref _selectedRalOuterFrame, value);

        }
    }
    public string? NewRalOuterFrameCode
    {
        get { return _newRalOuterFrameCode; }
        set { SetProperty(ref _newRalOuterFrameCode, value); }
    }
    public string? NewRalOuterFrameName
    {
        get { return _newRalOuterFrameName; }
        set { SetProperty(ref _newRalOuterFrameName, value); }
    }


    public ColorEntity? SelectedRalInnerSash
    {
        get { return _selectedRalInnerSash; }
        set
        {
            SetProperty(ref _selectedRalInnerSash, value);

        }
    }
    public string? NewRalInnerSashCode
    {
        get { return _newRalInnerSashCode; }
        set { SetProperty(ref _newRalInnerSashCode, value); }
    }
    public string? NewRalInnerSashName
    {
        get { return _newRalInnerSashName; }
        set { SetProperty(ref _newRalInnerSashName, value); }
    }


    public ColorEntity? SelectedRalOuterSash
    {
        get { return _selectedRalOuterSash; }
        set
        {
            SetProperty(ref _selectedRalOuterSash, value);

        }
    }
    public string? NewRalOuterSashCode
    {
        get { return _newRalOuterSashCode; }
        set { SetProperty(ref _newRalOuterSashCode, value); }
    }
    public string? NewRalOuterSashName
    {
        get { return _newRalOuterSashName; }
        set { SetProperty(ref _newRalOuterSashName, value); }
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

    private double _filterMinimumWidth;
    private double _filterMaximumWidth = 10000;

    private double _filterMinimumHeight;
    private double _filterMaximumHeight = 10000;

    private double _filterMinimumSquare;
    private double _filterMaximumSquare = 100000000;

    private double _filterMinimumLength;
    private double _filterMaximumLength = 100000;
    private string? _fillingType;
    private string? _glazingBeads;
    private string? _warmEdgeSpace;
    private double _calculatedPriceWidth;
    private double _calculatedPriceHeight;
    private double _calculatedPriceSquare;
    private double _calculatedPriceBase;
    private MeasurementEntity? _currentMeasurement;

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

                RalColour = CreateColor(NewColourCode, NewColourName, SelectedColour),
                RalSashColour = CreateColor(NewSashColourCode, NewSashColourName, SelectedSashColour),

                RalInnerFrame = CreateColor(NewRalInnerFrameCode, NewRalInnerFrameName, SelectedRalInnerFrame),
                RalOuterFrame = CreateColor(NewRalOuterFrameCode, NewRalOuterFrameName, SelectedRalOuterFrame),
                RalInnerSash = CreateColor(NewRalInnerSashCode, NewRalInnerSashName, SelectedRalInnerSash),
                RalOuterSash = CreateColor(NewRalOuterSashCode, NewRalOuterSashName, SelectedRalOuterSash),

                MinimumHeight = MinimumHeight,
                MaximumHeight = MaximumHeight,
                MinimumWidth = MinimumWidth,
                MaximumWidth = MaximumWidth,

                Bars = Bars,
                Pillars = Pillars
            };

            _dbContext.Measurements.Add(measurement);
            _dbContext.SaveChanges();

            _currentMeasurement = measurement;

            ProductPriceCollection prices = GetProductPrices();

            foreach (ProductPrice price in prices)
            {
                SizeMeasurementEntity sizeMeasurement = new SizeMeasurementEntity
                {
                    Measurement = _currentMeasurement,
                    Width = price.Width,
                    Height = price.Height,
                    Price = price.Price
                };


                _dbContext.SizeMeasurements.Add(sizeMeasurement);
            }
            _dbContext.SaveChanges();

            Refresh();
        }
        catch // (Exception e)
        {
            // do nothing
        }
    }

    protected virtual ProductPriceCollection GetProductPrices()
    {
        return new ProductPriceCollection();
    }

    public ICommand SaveCalculationCommand
    {
        get { return new TargetCommand(SaveCalculation, true); }
    }

    private void SaveCalculation()
    {
        try
        {
            if (_currentMeasurement != null)
            {
                return;
            }

            // SizeMeasurementEntity

            CalculationEntity calculation = new CalculationEntity
            {
                Measurement = _currentMeasurement,

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


        }
        catch
        {
            // do nothing
        }
    }


    private ProfileEntity? CreateProfile()
    {
        if (string.IsNullOrEmpty(NewProfileName))
        {
            return SelectedProfile;
        }

        if (SelectedBrand == null)
        {
            return null;
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

    private DistributionEntity? CreateDistribution()
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

    private GlassEntity? CreateGlass()
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

    private ColorEntity? CreateColor(string? newColorName, string? newColorCode, ColorEntity? selectedColor)
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