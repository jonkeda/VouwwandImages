using System.ComponentModel.DataAnnotations;

namespace VouwwandImages.Database;

public class SizeMeasurementEntity
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    public double Width { get; set; }
    public double Height { get; set; }
    public double Price { get; set; }

}