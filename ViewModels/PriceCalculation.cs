using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using VouwwandImages.Extensions;
using VouwwandImages.UI;
using VouwwandImages.UI.Graphs;

namespace VouwwandImages.ViewModels;

public class PriceCalculation : ViewModel
{
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

    private ProductPriceCollection GetProductPrices()
    {
        var priceText = PriceInput;

        var prices = new ProductPriceCollection();

        foreach (string line in priceText.Lines())
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                string[] values = line.Split('\t');
                prices.Add(new ProductPrice(int.Parse(values[0]), int.Parse(values[1]), double.Parse(values[2])));
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
        double maxBasePrice = 100;

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


        PriceResults = GetPriceResults(prices, minimumLengthPrice, minimumPlusPrice, minimumBasePrice, bars, pillars);

        /*        (minimumWidthPrice, minimumHeightPrice, minimumBasePrice, minimumPriceDifference) = CalculatePricesV2(sb,
                    prices,
                    0, 2000,
                    minimumWidthPrice - 10, minimumWidthPrice + 10,
                    minimumHeightPrice - 10, minimumHeightPrice + 10,
                    1);
        */


        PriceOutcome = sb.ToString();
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

    #region Stepping

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
}

public class ProductPricePrediction
{
    [ColumnName("Score")]
    public float Score;

    [ColumnName("Label")]
    public float PriceF;

}