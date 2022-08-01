using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VouwwandImages.Extensions;
using VouwwandImages.UI;

namespace VouwwandImages.ViewModels;

public class PriceCalculation : ViewModel
{
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

        (double minimumWidthPrice, double minimumHeightPrice, double minimumBasePrice, double minimumPriceDifference) = CalculatePrices(sb, 
            prices,
            0, 2000,
            0, 1000,
            0, 1000,
            10);

        (minimumWidthPrice, minimumHeightPrice, minimumBasePrice, minimumPriceDifference) = CalculatePrices(sb,
            prices,
            0, 2000,
            minimumWidthPrice - 10, minimumWidthPrice + 10,
            minimumHeightPrice - 10, minimumHeightPrice + 10,
            1);

        sb.AppendLine($"{minimumWidthPrice}\t{minimumHeightPrice}\t{minimumBasePrice}\t{minimumPriceDifference:0}");

        PriceOutcome = sb.ToString();
    }
    private (double minimumWidthPrice, double minimumHeightPrice, double minimumBasePrice, double minimumPriceDifference) CalculatePrices(StringBuilder sb,
        ProductPrice[] prices,
        double minBase, double maxBase,
        double minWidth, double maxWidth,
        double minHeight, double maxHeight,
        double step)
    {
        double minimumPriceDifference = double.MaxValue;
        double minimumHeightPrice = 0;
        double minimumWidthPrice = 0;
        double minimumBasePrice = 0;

        for (double basePrice = minBase; basePrice < maxBase; basePrice += 10)
        {
            for (double widthPrice = minWidth; widthPrice < maxWidth; widthPrice += step)
            {
                for (double heightPrice = minHeight; heightPrice < maxHeight; heightPrice += step)
                {
                    double totalPrice = 0;
                    foreach (ProductPrice p in prices)
                    {
                        double price = heightPrice * p.Height + widthPrice * p.Width + basePrice;
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


    #region Stepping

    private string _priceOutcome = "";
    private string _priceInput = "";

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