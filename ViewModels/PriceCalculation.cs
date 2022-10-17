using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VouwwandImages.Extensions;
using VouwwandImages.UI;
using VouwwandImages.UI.Graphs;
using VouwwandImages.ViewModels.Calculations;
using GeneticSharp.Extensions;

namespace VouwwandImages.ViewModels;

public class PriceCalculatedCollection : Collection<PriceCalculated>
{}

public class PriceCalculated
{
    public double WidthPrice { get; }
    public double HeightPrice { get; }
    public double SquarePrice { get; }
    public double BasePrice { get; }
    public double PriceDifference { get; }

    public PriceCalculated(double minimumWidthPrice, 
        double minimumHeightPrice, 
        double minimumSquarePrice, 
        double minimumBasePrice, 
        double minimumPriceDifference)
    {
        WidthPrice = minimumWidthPrice;
        HeightPrice = minimumHeightPrice;
        SquarePrice = minimumSquarePrice;
        BasePrice = minimumBasePrice;
        PriceDifference = minimumPriceDifference;
    }

    public override string ToString()
    {
        return PriceDifference.ToString();
    }
}

public class PriceCalculation : ViewModel
{
    #region Properties

    private string _bars = "1";
    private int _barsCount;
    private string _pillars = "1";
    private int _pillarsCount;

    public string Bars
    {
        get { return _bars; }
        set
        {
            if (SetProperty(ref _bars, value))
            {
                _barsCount = int.Parse(_bars);
            }
        }
    }

    public string Pillars
    {
        get { return _pillars; }
        set
        {
            if (SetProperty(ref _pillars, value))
            {
                _pillarsCount = int.Parse(_pillars);
            }
        }
    }

    public double MinimumPrice
    {
        get { return _minimumPrice; }
        set { SetProperty(ref _minimumPrice, value); }
    }

    public double MaximumPrice
    {
        get { return _maximumPrice; }
        set { SetProperty(ref _maximumPrice, value); }
    }


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

    #region Calculate Prices

    private ProductPriceCollection GetProductPrices()
    {
        var priceText = PriceInput;

        var prices = new ProductPriceCollection();

        foreach (string line in priceText.Lines())
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                string[] values = line.Split('\t');
                var price = new ProductPrice(int.Parse(values[0]), int.Parse(values[1]), double.Parse(values[2]));
                if (price.WidthI >= FilterMinimumWidth && price.WidthI <= FilterMaximumWidth
                       && price.HeightI >= FilterMinimumHeight&& price.HeightI <= FilterMaximumHeight
                       && price.SquareI >= FilterMinimumSquare && price.SquareI <= FilterMaximumSquare
                       && price.LengthI >= FilterMinimumLength && price.LengthI <= FilterMaximumLength)
                {
                    prices.Add(price);
                }
            }
        }
        return prices;
    }

    protected void CalculatePrices()
    {
        PriceOutcome = "";

        StringBuilder sb = new StringBuilder();
        var prices = GetProductPrices();

        int bars = _barsCount;
        int pillars = _pillarsCount;
        double minPrice = MinimumPrice;
        double maxPrice = MaximumPrice;
        double stepPrice = maxPrice / 16;
        //double stepPrice = double.Parse(StepPrice);
        /*

                (double minimumWidthPrice, double minimumHeightPrice, double minimumBasePrice, double minimumPriceDifference) = CalculatePrices(sb,
                    prices,
                    0, maxBasePrice * 2,
                    0, maxBasePrice,
                    0, maxBasePrice,
                    10,
                    bars, pillars);

                (minimumWidthPrice, minimumHeightPrice, minimumBasePrice, minimumPriceDifference) = CalculatePrices(sb,
                    prices,
                    0, maxBasePrice * 2,
                    minimumWidthPrice - 10, minimumWidthPrice + 10,
                    minimumHeightPrice - 10, minimumHeightPrice + 10,
                    1,
                    bars, pillars);

                sb.AppendLine($"{minimumWidthPrice}\t{minimumHeightPrice}\t{minimumBasePrice}\t{minimumPriceDifference:0}");


                (var minimumLengthPrice, var minimumPlusPrice, minimumBasePrice, minimumPriceDifference) = CalculatePricesV2(sb,
                    prices,
                    0, maxBasePrice,
                    0, maxBasePrice,
                    0, maxBasePrice,
                    1,
                    bars, pillars);
                sb.AppendLine($"{minimumLengthPrice}\t{minimumPlusPrice:F1}\t{minimumBasePrice}\t{minimumPriceDifference:0}");
        */

        /* PriceResults = GetPriceResults(prices, minimumLengthPrice, minimumPlusPrice, minimumBasePrice, bars, pillars); */

        /*        (minimumWidthPrice, minimumHeightPrice, minimumBasePrice, minimumPriceDifference) = CalculatePricesV2(sb,
                    prices,
                    0, 2000,
                    minimumWidthPrice - 10, minimumWidthPrice + 10,
                    minimumHeightPrice - 10, minimumHeightPrice + 10,
                    1);
        */
        double magnify = 2;
        DateTime start = DateTime.Now;
        var calculatedPrices = CalculatePricesV3(sb,
            prices,
            minPrice, maxPrice,
            minPrice, maxPrice,
            minPrice, maxPrice,
            minPrice, maxPrice,
                stepPrice,
                bars, pillars);
        sb.AppendLine((DateTime.Now - start).TotalMilliseconds.ToString());
        start = DateTime.Now;
        foreach (var c in calculatedPrices)
        {
            sb.AppendLine($"w: {c.WidthPrice:F2}\th: {c.HeightPrice:F2}\tS: {c.SquarePrice:F2}\tB:{c.BasePrice:F2}\tD: {c.PriceDifference:F2}");
        }

        var startMax = maxPrice;
        maxPrice /= magnify;
        // stepPrice /= magnify;

        for (int i = 0; i < 10; i++)
        {
            var minimum = calculatedPrices.First();

            calculatedPrices = CalculatePricesV3(sb,
                prices,
                Math.Max(0, minimum.WidthPrice - maxPrice), Math.Min(startMax, minimum.WidthPrice + maxPrice),
                Math.Max(0, minimum.HeightPrice - maxPrice), Math.Min(startMax, minimum.HeightPrice + maxPrice),
                Math.Max(0, minimum.SquarePrice - maxPrice), Math.Min(startMax, minimum.SquarePrice + maxPrice),
                Math.Max(0, minimum.BasePrice - maxPrice), Math.Min(startMax, minimum.BasePrice + maxPrice),
                stepPrice,
                bars, pillars);

            sb.AppendLine();
            sb.AppendLine((DateTime.Now - start).TotalMilliseconds.ToString());
            start = DateTime.Now;
            foreach (var c in calculatedPrices)
            {
                sb.AppendLine($"w: {c.WidthPrice:F2}\th: {c.HeightPrice:F2}\tS: {c.SquarePrice:F2}\tB:{c.BasePrice:F2}\tD: {c.PriceDifference:F2}");
            }
            maxPrice /= magnify;
            stepPrice /= magnify;

        }

        // sb.AppendLine($"{minimumWidthPrice:F0}\t{minimumHeightPrice:F0}\t{minimumSquarePrice:F0}\t{minimumBasePrice:F0}");

        /*
                sb.AppendLine($"w: {minimumWidthPrice:F2} h: {minimumHeightPrice:F2} S: {minimumSquarePrice:F2} B:{minimumBasePrice:F2} D: {minimumPriceDifference:F2}");
                sb.AppendLine($"{minimumWidthPrice:F0}\t{minimumHeightPrice:F0}\t{minimumSquarePrice:F0}\t{minimumBasePrice:F0}");

                (minimumWidthPrice, minimumHeightPrice, minimumSquarePrice, minimumBasePrice, minimumPriceDifference) = CalculatePricesV3(sb,
                    prices,
                    minimumWidthPrice - stepPrice, minimumWidthPrice + stepPrice,
                    minimumHeightPrice - stepPrice, minimumHeightPrice + stepPrice,
                    minimumSquarePrice - stepPrice, minimumSquarePrice + stepPrice,
                    minimumBasePrice - stepPrice, minimumBasePrice + stepPrice,
                    stepPrice = stepPrice / 10,
                    bars, pillars);
                sb.AppendLine($"w: {minimumWidthPrice:F2} h: {minimumHeightPrice:F2} S: {minimumSquarePrice:F2} B:{minimumBasePrice:F2} D: {minimumPriceDifference:F2}");
                sb.AppendLine($"{minimumWidthPrice:F2}\t{minimumHeightPrice:F2}\t{minimumSquarePrice:F2}\t{minimumBasePrice:F2}");

                (minimumWidthPrice, minimumHeightPrice, minimumSquarePrice, minimumBasePrice, minimumPriceDifference) = CalculatePricesV3(sb,
                    prices,
                    minimumWidthPrice - stepPrice, minimumWidthPrice + stepPrice,
                    minimumHeightPrice - stepPrice, minimumHeightPrice + stepPrice,
                    minimumSquarePrice - stepPrice, minimumSquarePrice + stepPrice,
                    minimumBasePrice - stepPrice, minimumBasePrice + stepPrice,
                    stepPrice = stepPrice / 10,
                    bars, pillars);
                sb.AppendLine($"w: {minimumWidthPrice:F2} h: {minimumHeightPrice:F2} S: {minimumSquarePrice:F2} B:{minimumBasePrice:F2} D: {minimumPriceDifference:F2}");
                sb.AppendLine($"{minimumWidthPrice:F2}\t{minimumHeightPrice:F2}\t{minimumSquarePrice:F2}\t{minimumBasePrice:F2}");
        */

        var mc = calculatedPrices.First();
        PriceOutcome =
            $"{mc.WidthPrice:F2}\t{mc.HeightPrice:F2}\t{mc.SquarePrice:F2}\t{mc.BasePrice:F2}\t{mc.PriceDifference:F2}\n"
            + sb;
    }

    private string GetPriceResults(ProductPriceCollection prices, double lengthPrice, double plusPrice, double basePrice,
        int bars, int pillars)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var p in prices)
        {
            double length = p.Height * pillars + p.Width * bars;

            double price = ((length * plusPrice) + lengthPrice) * length + basePrice;

            sb.AppendLine($"{p.Width:F1}\t{p.Height}\t{p.Price}\t{price:0}\t{p.Price - price:F0}");
        }

        return sb.ToString();
    }

    private (double minimumWidthPrice, double minimumHeightPrice, double minimumBasePrice, double minimumPriceDifference)
        CalculatePrices(StringBuilder sb,
            ProductPriceCollection prices,
        double minBase, double maxBase,
        double minWidth, double maxWidth,
        double minHeight, double maxHeight,
        double step,
        int bars, int pillars)
    {
        double minimumPriceDifference = double.MaxValue;
        double minimumHeightPrice = 0;
        double minimumWidthPrice = 0;
        double minimumBasePrice = 0;

        for (double basePrice = minBase; basePrice < maxBase; basePrice += step)
        {
            for (double widthPrice = minWidth; widthPrice < maxWidth; widthPrice += step)
            {
                for (double heightPrice = minHeight; heightPrice < maxHeight; heightPrice += step)
                {
                    double totalPrice = 0;
                    foreach (ProductPrice p in prices)
                    {
                        double price = heightPrice * pillars * p.Height + widthPrice * bars * p.Width + basePrice;
                        totalPrice += Math.Abs(p.Price - price);
                    }

                    if (minimumPriceDifference > totalPrice)
                    {
                        minimumPriceDifference = totalPrice;

                        minimumHeightPrice = heightPrice;
                        minimumWidthPrice = widthPrice;
                        minimumBasePrice = basePrice;
                    }
                }
            }
        }

        return (minimumWidthPrice, minimumHeightPrice, minimumBasePrice, minimumPriceDifference);
    }

    private (double minimumWidthPrice, double minimumHeightPrice, double minimumBasePrice, double minimumPriceDifference)
        CalculatePricesV2(StringBuilder sb,
            ProductPriceCollection prices,
        double minBase, double maxBase,
        double minWidth, double maxWidth,
        double minHeight, double maxHeight,
        double step,
        int bars, int pillars)
    {
        double minimumPriceDifference = double.MaxValue;
        double minimumLengthPrice = 0;
        double minimumPlusPrice = 0;
        double minimumBasePrice = 0;

        double minLength = minWidth + minHeight;
        double maxLength = maxWidth + maxHeight;

        for (double basePrice = minBase; basePrice < maxBase; basePrice += step)
        {
            for (double lengthPrice = minLength; lengthPrice < maxLength; lengthPrice += step)
            {
                for (double plusPrice = minLength / 100; plusPrice < maxLength / 100; plusPrice += step / 100)
                {
                    double totalPrice = 0;
                    foreach (ProductPrice p in prices)
                    {
                        double length = p.Height * pillars + p.Width * bars;

                        double price = ((length * plusPrice) + lengthPrice) * length + basePrice;

                        totalPrice += Math.Abs(p.Price - price);
                    }

                    if (minimumPriceDifference > totalPrice)
                    {
                        minimumPriceDifference = totalPrice;

                        minimumLengthPrice = lengthPrice;
                        minimumPlusPrice = plusPrice;
                        minimumBasePrice = basePrice;
                    }
                }
            }
        }

        return (minimumLengthPrice, minimumPlusPrice, minimumBasePrice, minimumPriceDifference);
    }

    #endregion

    #region Calculate Prices V3

    // 66B+70A+10BA+54
    private List<PriceCalculated> CalculatePricesV3(StringBuilder sb,
            ProductPriceCollection prices,
            double minWidth, double maxWidth,
            double minHeight, double maxHeight,
            double minSquare, double maxSquare,
            double minBase, double maxBase,
            double step,
            int bars, int pillars)
    {
        double minimumPriceDifference = double.MaxValue;

/*        double minimumWidthPrice = 0;
        double minimumHeightPrice = 0;
        double minimumSquarePrice = 0;
        double minimumBasePrice = 0;
*/
        List<PriceCalculated> calculatedPrices = new List<PriceCalculated>();
        for (double basePrice = minBase; basePrice < maxBase; basePrice += step)
        {
            for (double widthPrice = minWidth; widthPrice < maxWidth; widthPrice += step)
            {
                for (double heightPrice = minHeight; heightPrice < maxHeight; heightPrice += step)
                {
                    for (double squarePrice = minSquare; squarePrice < maxSquare; squarePrice += step)
                    {

                        double totalPrice = 0;
                        foreach (ProductPrice p in prices)
                        {
                            double fullWidth = p.Width * bars;
                            double fullHeight = p.Height * pillars;
                            double fullSquare = p.Height * p.Width;
                            //double length = p.Height * pillars + p.Width * bars;

                            double price = fullWidth * widthPrice 
                                + fullHeight * heightPrice
                                + fullSquare * squarePrice
                                + basePrice;

                            totalPrice += Math.Abs(p.Price - price);
                        }

                        if (calculatedPrices.Count < 2
                            || minimumPriceDifference > totalPrice)
                        {
                            calculatedPrices.Add(new PriceCalculated(widthPrice, 
                                heightPrice, 
                                squarePrice, 
                                basePrice, 
                                totalPrice));
                            calculatedPrices = calculatedPrices.OrderBy(c => c.PriceDifference).ToList();

                            if (calculatedPrices.Count > 2)
                            {
                                calculatedPrices.RemoveAt(calculatedPrices.Count - 1);
                            }
                            minimumPriceDifference = calculatedPrices.Last().PriceDifference;
                        }
                    }

                }
            }
        }

        return calculatedPrices;
    }
    #endregion

    #region Plot prices

    private string _priceOutcome = "";
    private string _priceInput = "";
    private string _priceResults = "";
    private PlotData? _byHeightPlot;
    private PlotData? _byWidthPlot;

    private PlotData? _byHeightDifference;
    private PlotData? _byWidthDifference;
    private string _text;
    private PlotData? _byLength;

    private PlotData? _byLengthPerMeter;
    private PlotData? _byWidthPerMeter;
    private PlotData? _byHeightPerMeter;
    private FunctionBuilderRunner? _builder;

    private double _maximumOperations = 11;
    private double _minimumPrice = 0;
    private double _maximumPrice = 2048;

    private double _filterMinimumWidth = 0;
    private double _filterMaximumWidth = 10000;

    private double _filterMinimumHeight = 0;
    private double _filterMaximumHeight = 10000;

    private double _filterMinimumSquare = 0;
    private double _filterMaximumSquare = 100000000;

    private double _filterMinimumLength = 0;
    private double _filterMaximumLength = 10000;

    public string PriceInput
    {
        get { return _priceInput; }
        set { SetProperty(ref _priceInput, value); }
    }

    public string PriceOutcome
    {
        get { return _priceOutcome; }
        set { SetProperty(ref _priceOutcome, value); }
    }

    public string PriceResults
    {
        get { return _priceResults; }
        set { SetProperty(ref _priceResults, value); }
    }

    public ICommand CalculatePriceCommand
    {
        get { return new TargetCommand(CalculatePrice, true); }
    }

    private void CalculatePrice()
    {
        CalculatePrices();
    }

    #endregion

    #region PlotData

    public PlotData? ByHeight
    {
        get { return _byHeightPlot; }
        set { SetProperty(ref _byHeightPlot, value); }
    }

    public PlotData? ByWidth
    {
        get { return _byWidthPlot; }
        set { SetProperty(ref _byWidthPlot, value); }
    }

    public PlotData? ByLength
    {
        get { return _byLength; }
        set { SetProperty(ref _byLength, value); }
    }

    public PlotData? ByLengthPerMeter
    {
        get { return _byLengthPerMeter; }
        set { SetProperty(ref _byLengthPerMeter, value); }
    }

    public PlotData? ByHeightPerMeter
    {
        get { return _byHeightPerMeter; }
        set { SetProperty(ref _byHeightPerMeter, value); }
    }

    public PlotData? ByWidthPerMeter
    {
        get { return _byWidthPerMeter; }
        set { SetProperty(ref _byWidthPerMeter, value); }
    }

    public PlotData? ByHeightDifference
    {
        get { return _byHeightDifference; }
        set { SetProperty(ref _byHeightDifference, value); }
    }

    public PlotData? ByWidthDifference
    {
        get { return _byWidthDifference; }
        set { SetProperty(ref _byWidthDifference, value); }
    }

    public ICommand CreateGraphCommand
    {
        get { return new TargetCommand(CreateGraph, true); }
    }

    public string Text
    {
        get { return _text; }
        set { SetProperty(ref _text, value); }
    }

    private void CreateGraph()
    {
        var prices = GetProductPrices();

        ByHeight = GetDataPrice("By height", prices.OrderBy(p => p.Width).ThenBy(p => p.Height),
            p => p.Height, p => p.Width, p => p.Price);
        ByWidth = GetDataPrice("By width", prices.OrderBy(p => p.Height).ThenBy(p => p.Width),
            p => p.Width, p => p.Height, p => p.Price);


        ByHeightDifference = CalculatePriceDifference("By height difference", prices.OrderBy(p => p.Width).ThenBy(p => p.Height),
            p => p.Height, p => p.Width);
        ByWidthDifference = CalculatePriceDifference("By width difference", prices.OrderBy(p => p.Height).ThenBy(p => p.Width),
            p => p.Width, p => p.Height);


        ByHeightPerMeter = CalculatePricePerMeter("By height per meter", prices.OrderBy(p => p.Width).ThenBy(p => p.Height),
            p => p.Height, p => p.Width);
        ByWidthPerMeter = CalculatePricePerMeter("By width per meter", prices.OrderBy(p => p.Height).ThenBy(p => p.Width),
            p => p.Width, p => p.Height);


        ByLength = GetDataPrice("By length", prices.OrderBy(p => p.Length),
            p => p.Length, p => 0, p => p.Price);

        (ProductPrice? first, ProductPrice? last) = GetFirstLastProductPrice(prices);

        double startPrice = CalculateStartPrice(first, last);

        ByLengthPerMeter = GetDataPrice("By length per meter", prices.OrderBy(p => p.Length),
            p => p.Length, p => 0, p => (p.Price - startPrice) / p.Length);
    }

    private double CalculateStartPrice(ProductPrice first, ProductPrice last)
    {
        var priceDiff = last.Price - first.Price;
        var lengthDiff = last.Length - first.Length;

        var priceLength = priceDiff / lengthDiff;

        var firstPrice = priceLength * first.Length;
        return first.Price - firstPrice;
    }

    private (ProductPrice? first, ProductPrice? last) GetFirstLastProductPrice(ProductPriceCollection prices)
    {
        var orderedPrices = prices.OrderBy(p => p.Length);
        return (orderedPrices.FirstOrDefault(), orderedPrices.LastOrDefault());
    }

    private PlotData CalculatePriceDifference(string title, IEnumerable<ProductPrice> prices, Func<ProductPrice, double> x, Func<ProductPrice, double> z)
    {
        ProductPriceCollection newPrices = new();

        double zCurrent = double.MinValue;
        //double xCurrent = double.MinValue;
        double priceCurrent = 0;

        foreach (var price in prices)
        {
            //double xValue = x(price);
            double zValue = z(price);
            if (zCurrent != zValue)
            {
                zCurrent = zValue;
                //xCurrent = xValue;
                priceCurrent = price.Price;
            }
            newPrices.Add(new ProductPrice(price.Width, price.Height, price.Price - priceCurrent));
            priceCurrent = price.Price;
        }

        return GetDataPrice(title, newPrices, x, z, p => p.Price);
    }

    private PlotData CalculatePricePerMeter(string title, IEnumerable<ProductPrice> prices, Func<ProductPrice, double> x, Func<ProductPrice, double> z)
    {
        ProductPriceCollection newPrices = new();

        double zCurrent = double.MinValue;
        double xCurrent = double.MinValue;
        double priceCurrent = 0;

        foreach (var price in prices)
        {
            double xValue = x(price);
            double zValue = z(price);
            if (zCurrent != zValue)
            {
                zCurrent = zValue;
                xCurrent = xValue;
                priceCurrent = price.Price;
            }

            var lengthDifference = xValue - xCurrent;
            if (lengthDifference != 0)
            {
                newPrices.Add(new ProductPrice(price.Width, price.Height, (price.Price - priceCurrent) / lengthDifference));
            }
            xCurrent = xValue;
            priceCurrent = price.Price;
        }

        return GetDataPrice(title, newPrices, x, z, p => p.Price);
    }

    private PlotData GetDataPrice(string title, IEnumerable<ProductPrice> prices, Func<ProductPrice, double> x, Func<ProductPrice, double> z, Func<ProductPrice, double> p)
    {
        List<double> dataX = new();
        List<double> dataY = new();
        double zCurrent = double.MinValue;
        double xCurrent = double.MinValue;

        ScatterDataCollection scatterDatas = new ScatterDataCollection();
        ScatterData data = new ScatterData();

        foreach (var price in prices)
        {
            double xValue = x(price);
            double zValue = z(price);
            if (zCurrent != zValue)
            {
                if (dataX.Count > 1)
                {
                    scatterDatas.Add(new ScatterData(zCurrent.ToString(), dataX, dataY));
                    dataX = new();
                    dataY = new List<double>();
                }
                zCurrent = zValue;
                xCurrent = xValue;
            }

            if (xCurrent != xValue)
            {
                dataX.Add(xValue);
                dataY.Add(p(price));
            }
        }

        if (dataX.Count > 1)
        {
            scatterDatas.Add(new ScatterData(zCurrent.ToString(), dataX, dataY));
        }

        return new PlotData(scatterDatas) { Title = title };
    }

    #endregion


    #region ML.NET

    public ICommand CreateAICommand
    {
        get { return new TargetCommand(CreateAI, true); }
    }

    private void CreateAI()
    {
        //Create ML Context with seed for repeteable/deterministic results
        MLContext mlContext = new MLContext(seed: 0);

        // STEP 1: Common data loading configuration
        IDataView baseTrainingDataView = mlContext.Data.LoadFromEnumerable(GetProductPrices());
        // mlContext.Data.LoadFromTextFile<ProductPrice>(TrainDataPath, hasHeader: true, separatorChar: ',');
        // IDataView testDataView = mlContext.Data.LoadFromTextFile<ProductPrice>(TestDataPath, hasHeader: true, separatorChar: ',');



        // Sample code of removing extreme data like "outliers" for FareAmounts higher than $150 and lower than $1 which can be error-data 
        //var cnt = baseTrainingDataView.GetColumn<float>(nameof(ProductPrice.Price)).Count();
        //IDataView trainingDataView = mlContext.Data.FilterRowsByColumn(baseTrainingDataView, nameof(ProductPrice.Price), lowerBound: 1, upperBound: 150);
        //var cnt2 = trainingDataView.GetColumn<float>(nameof(ProductPrice.Price)).Count();

        // STEP 2: Common data process configuration with pipeline data transformations
        var dataProcessPipeline = mlContext.Transforms
                                    .CopyColumns(outputColumnName: "Label", inputColumnName: nameof(ProductPrice.PriceF))
                                    //.Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "VendorIdEncoded", inputColumnName: nameof(TaxiTrip.VendorId)))
                                    //.Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "RateCodeEncoded", inputColumnName: nameof(TaxiTrip.RateCode)))
                                    //.Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "PaymentTypeEncoded", inputColumnName: nameof(TaxiTrip.PaymentType)))
                                    .Append(mlContext.Transforms.NormalizeMeanVariance(outputColumnName: nameof(ProductPrice.HeightF)))
                                    .Append(mlContext.Transforms.NormalizeMeanVariance(outputColumnName: nameof(ProductPrice.WidthF)))
                                    //.Append(mlContext.Transforms.NormalizeMeanVariance(outputColumnName: nameof(TaxiTrip.TripDistance)))
                                    .Append(mlContext.Transforms.Concatenate("Features",
                                        nameof(ProductPrice.HeightF), nameof(ProductPrice.WidthF), nameof(ProductPrice.PriceF)))
                                    ;


        // STEP 3: Set the training algorithm, then create and config the modelBuilder - Selected Trainer (SDCA Regression algorithm)                            
        var trainer = mlContext.Regression.Trainers.Sdca(labelColumnName: "Label", featureColumnName: "Features");
        var trainingPipeline = dataProcessPipeline.Append(trainer);

        // STEP 4: train
        var trainedModel = trainingPipeline.Fit(baseTrainingDataView);


        // mlContext.Model.Save(trainedModel, );

        // STEP 5: Predict

        var productSample = new ProductPrice(1.300d, 1.300d, 0);

        // Create prediction engine related to the loaded trained model
        var predEngine = mlContext.Model.CreatePredictionEngine<ProductPrice, ProductPricePrediction>(trainedModel);

        //Score
        var resultprediction = predEngine.Predict(productSample);
    }

    #endregion

    #region Function Builder

    public double MaximumOperations
    {
        get { return _maximumOperations; }
        set { SetProperty(ref _maximumOperations, value); }
    }

    public FunctionBuilderRunner? Builder
    {
        get { return _builder; }
        set { SetProperty(ref _builder, value); }
    }

    public ICommand CreateFunctionBuilderCommand
    {
        get { return new TargetTaskCommand(CreateFunctionBuilder); }
    }

    private async Task CreateFunctionBuilder()
    {
        List<FunctionBuilderInput> inputs = new List<FunctionBuilderInput>();
        foreach (var price in GetProductPrices())
        {
            FunctionBuilderInput input = new FunctionBuilderInput(new List<double>() { price.Height, price.Width }, price.Price);
            inputs.Add(input);
        }

        var maxOperations = (int)MaximumOperations;

        Builder = new FunctionBuilderRunner(new FunctionBuilder(inputs, maxOperations));
        await Task.Factory.StartNew(Builder.Run);
    }

    public ICommand StopFunctionBuilderCommand
    {
        get { return new TargetCommand(StopFunctionBuilder); }
    }

    private void StopFunctionBuilder()
    {
        Builder?.Stop();
    }

    public ICommand ResumeFunctionBuilderCommand
    {
        get { return new TargetCommand(ResumeFunctionBuilder); }
    }

    private void ResumeFunctionBuilder()
    {
        Builder?.Resume();
    }

    #endregion
}