using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VouwwandImages.Models.Measurements;

namespace VouwwandImages.Database;

public class MeasurementEntity : ISize
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    public int? BrandId { get; set; }
    [ForeignKey(nameof(BrandId))]
    public virtual BrandEntity? Brand { get; set; }

    public int? ProductTypeId { get; set; }
    [ForeignKey(nameof(ProductTypeId))]
    public virtual ProductTypeEntity? ProductType { get; set; }

    public int? ProfileId { get; set; }
    [ForeignKey(nameof(ProfileId))]
    public virtual ProfileEntity? Profile { get; set; }

    public int? DistributionId { get; set; }
    [ForeignKey(nameof(DistributionId))]
    public virtual DistributionEntity? Distribution { get; set; }

    public virtual List<SizeMeasurementEntity> Sizes { get; set; }


    // Sizes
    public double MinimumWidth { get; set; }

    public double MaximumWidth { get; set; }

    public double MinimumHeight { get; set; }

    public double MaximumHeight { get; set; }


    // Glass
    [StringLength(100)]
    public string? FillingType { get; set; }

    public int? GlazingId { get; set; }
    [ForeignKey(nameof(GlazingId))]
    public virtual GlassEntity? Glazing { get; set; }

    [StringLength(100)]
    public string? GlazingBeads { get; set; }

    [StringLength(100)]
    public string? WarmEdgeSpace { get; set; }


    // Colors
    public int? RalInsideId { get; set; }
    [ForeignKey(nameof(RalInsideId))]
    public virtual ColorEntity? RalInside { get; set; }

    public int? RalOutsideId { get; set; }
    [ForeignKey(nameof(RalOutsideId))]
    public virtual ColorEntity? RalOutside { get; set; }

    public int? RalSashInsideId { get; set; }
    [ForeignKey(nameof(RalSashInsideId))]
    public virtual ColorEntity? RalSashInside { get; set; }
    
    public int? RalSashOutsideId { get; set; }
    [ForeignKey(nameof(RalSashOutsideId))]
    public virtual ColorEntity? RalSashOutside { get; set; }


    // Filters
    public double FilterMinimumWidth { get; set; }
    public double FilterMaximumWidth { get; set; }
    public double FilterMinimumHeight { get; set; }
    public double FilterMaximumHeight { get; set; }

    public double FilterMinimumSquare { get; set; }
    public double FilterMaximumSquare { get; set; }
    public double FilterMinimumLength { get; set; }
    public double FilterMaximumLength { get; set; }
}