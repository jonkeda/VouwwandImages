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

    // 
    public decimal Bars { get; set; }

    public decimal Pillars { get; set; }

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

    public int? RalColourId { get; set; }
    [ForeignKey(nameof(RalColourId))]
    public virtual ColorEntity? RalColour { get; set; }

    public int? RalSashColourId { get; set; }
    [ForeignKey(nameof(RalSashColourId))]
    public virtual ColorEntity? RalSashColour { get; set; }

    public int? RalInnerFrameId { get; set; }
    [ForeignKey(nameof(RalInnerFrameId))]
    public virtual ColorEntity? RalInnerFrame { get; set; }

    public int? RalOuterFrameId { get; set; }
    [ForeignKey(nameof(RalOuterFrameId))]
    public virtual ColorEntity? RalOuterFrame { get; set; }

    public int? RalInnerSashId { get; set; }
    [ForeignKey(nameof(RalInnerSashId))]
    public virtual ColorEntity? RalInnerSash { get; set; }
    
    public int? RalOuterSashId { get; set; }
    [ForeignKey(nameof(RalOuterSashId))]
    public virtual ColorEntity? RalOuterSash { get; set; }


    public int? RalInnerFittingId { get; set; }
    [ForeignKey(nameof(RalInnerFittingId))]
    public virtual ColorEntity? RalInnerFitting { get; set; }

    public int? RalOuterFittingId { get; set; }
    [ForeignKey(nameof(RalOuterFittingId))]
    public virtual ColorEntity? RalOuterFitting { get; set; }

    public int? RalInnerPeripheralId { get; set; }
    [ForeignKey(nameof(RalInnerPeripheralId))]
    public virtual ColorEntity? RalInnerPeripheral { get; set; }

    public int? RalOuterPeripheralId { get; set; }
    [ForeignKey(nameof(RalOuterPeripheralId))]
    public virtual ColorEntity? RalOuterPeripheral { get; set; }

}