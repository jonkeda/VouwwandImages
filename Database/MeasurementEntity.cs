using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VouwwandImages.Models.Measurements;

namespace VouwwandImages.Database;

public class MeasurementEntity : ISize
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    public int?  BrandId { get; set; }
    public virtual BrandEntity? Brand { get; set; }

    public int? ProductTypeId { get; set; }
    public virtual ProductTypeEntity? ProductType { get; set; }

    public virtual List<SizeMeasurementEntity> Sizes { get; set; }

    public double MinimumWidth { get; set; }

    public double MaximumWidth { get; set; }

    public double MinimumHeight { get; set; }

    public double MaximumHeight { get; set; }

}

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
