using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VouwwandImages.Extensions;
using VouwwandImages.UI;

namespace VouwwandImages.ViewModels;

public class PriceCalculation : ViewModel
{
    private string _bars;
    private string _pillars;

    public string Bars
    {
        get { return _bars; }
        set { SetProperty(ref _bars, value); }
    }

    public string Pillars
    {
        get { return _pillars; }
        set { SetProperty(ref _pillars, value); }
    }

    private ProductPrice[] GetProductPrices()
    {
        var priceText = PriceInput;

        List<ProductPrice> prices = new List<ProductPrice>();

        foreach (string line in priceText.Lines())
        {
            if (!string.IsNullOrEmpty(line))
            {
                string[] values = line.Split('\t');
                prices.Add(new ProductPrice(int.Parse(values[0]), int.Parse(values[1]), double.Parse(values[2])));
            }
        }

        return prices.ToArray();
    }

    protected void CalculatePrices()
    {
        PriceOutcome = "";

        StringBuilder sb = new StringBuilder();

        var prices = GetProductPrices();

        int bars = int.Parse(Bars);
        int pillars = int.Parse(Pillars);

        (double minimumWidthPrice, double minimumHeightPrice, double minimumBasePrice, double minimumPriceDifference) = CalculatePrices(sb, 
            prices,
            0, 2000,
            0, 1000,
            0, 1000,
            10,
            bars, pillars);

        (minimumWidthPrice, minimumHeightPrice, minimumBasePrice, minimumPriceDifference) = CalculatePrices(sb,
            prices,
            0, 2000,
            minimumWidthPrice - 10, minimumWidthPrice + 10,
            minimumHeightPrice - 10, minimumHeightPrice + 10,
            1,
            bars, pillars);
        
        sb.AppendLine($"{minimumWidthPrice}\t{minimumHeightPrice}\t{minimumBasePrice}\t{minimumPriceDifference:0}");


        (var minimumLengthPrice, var minimumPlusPrice, minimumBasePrice, minimumPriceDifference) = CalculatePricesV2(sb,
            prices,
            0, 100,
            0, 100,
            0, 100,
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

    private string GetPriceResults(ProductPrice[] prices, double lengthPrice, double plusPrice, double basePrice,
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
        ProductPrice[] prices,
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
        ProductPrice[] prices,
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
        get { return new TargetCommand(CalculatePrice); }
    }

    private void CalculatePrice()
    {
        CalculatePrices();
    }
    
    #endregion

}