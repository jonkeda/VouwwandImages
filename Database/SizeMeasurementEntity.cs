using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VouwwandImages.Database;

public class SizeMeasurementEntity
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    public int? MeasurementId { get; set; }
    [ForeignKey(nameof(MeasurementId))]
    public virtual MeasurementEntity? Measurement { get; set; }

    public double Width { get; set; }
    public double Height { get; set; }
    public double Price { get; set; }

}