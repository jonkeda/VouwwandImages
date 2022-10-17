using Microsoft.ML.Data;

namespace VouwwandImages.ViewModels;

public class ProductPricePrediction
{
    [ColumnName("Score")]
    public float Score;

    [ColumnName("Label")]
    public float PriceF;

}