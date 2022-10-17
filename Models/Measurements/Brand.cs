using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using VouwwandImages.Database;

namespace VouwwandImages.Models.Measurements
{
    public class SizeMeasurementCollection : Collection<SizeMeasurement>
    {

    }

    public class SizeMeasurement
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public double Price { get; set; }
    }

    public interface ISize
    {
        double MinimumWidth { get; set; }

        double MaximumWidth { get; set; }

        double MinimumHeight { get; set; }

        double MaximumHeight { get; set; }

    }

    public interface IGlazing
    {
        string FillingType { get; set; }
        string Glazing { get; set; }
        string GlazingBeads { get; set; }
        string WarmEdgeSpace { get; set; }
    }

    public class Measurement : ISize
    {
        public Brand Brand { get; set; }

        public ProductType ProductType { get; set; }

        public SizeMeasurementCollection Sizes { get; set; } = new();

        public double MinimumWidth { get; set; }

        public double MaximumWidth { get; set; }

        public double MinimumHeight { get; set; }

        public double MaximumHeight { get; set; }
    }
    
}