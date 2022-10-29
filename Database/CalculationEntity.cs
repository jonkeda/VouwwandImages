using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VouwwandImages.Database;

public class CalculationEntity
{
    [Key]
    public int Id { get; set; }


    public int? MeasurementId { get; set; }
    [ForeignKey(nameof(MeasurementId))]
    public virtual MeasurementEntity? Measurement { get; set; }

    // Filters
    public double FilterMinimumWidth { get; set; }
    public double FilterMaximumWidth { get; set; }
    public double FilterMinimumHeight { get; set; }
    public double FilterMaximumHeight { get; set; }

    public double FilterMinimumSquare { get; set; }
    public double FilterMaximumSquare { get; set; }
    public double FilterMinimumLength { get; set; }
    public double FilterMaximumLength { get; set; }


    public double CalculatedPriceWidth { get; set; }
    public double CalculatedPriceHeight { get; set; }
    public double CalculatedPriceSquare { get; set; }
    public double CalculatedPriceBase { get; set; }

}