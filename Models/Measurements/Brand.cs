using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

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

    public class MeasurementGlazing
    {

    }

    public interface ISize
    {
        double MinimumWidth { get; set; }

        double MaximumWidth { get; set; }

        double MinimumHeight { get; set; }

        double MaximumHeight { get; set; }

    }

    public class Measurement : ISize
    {
        public Brand Brand { get; set; }

        public ProductType ProductType { get; set; }

        public SizeMeasurementCollection Sizes { get; set; }

        public double MinimumWidth { get; set; }

        public double MaximumWidth { get; set; }

        public double MinimumHeight { get; set; }

        public double MaximumHeight { get; set; }
    }

    public enum ProductType
    {
        [Display(Name = "Windows")]
        Windows = 0,

        [Display(Name = "Balcony")]
        Balcony = 1,

        [Display(Name = "Doors")]
        Doors = 2,

        [Display(Name = "Tilt and slide windows")]
        TiltAndSlideWindows = 3,

        [Display(Name = "Sliding Systems")]
        SlidingSystems = 4,

        [Display(Name = "Folding Doors")]
        FoldingDoors = 5,

        [Display(Name = "Interior Doors")]
        InteriorDoors = 6,

        [Display(Name = "Garage Doors")]
        GarageDoors = 7,

        [Display(Name = "Roller Shutters")]
        RollerShutters = 8,

        [Display(Name = "External Blinds")]
        ExternalBlinds = 9,

        [Display(Name = "Mosquito Nets")]
        MosquitoNets = 10,

        [Display(Name = "Windows Shutters")]
        WindowsShutters = 11,

        [Display(Name = "Tech Screens")]
        TechScreens = 12,

        [Display(Name = "Fences")]
        Fences = 13,

        [Display(Name = "Glazing")]
        Glazing = 14,

        [Display(Name = "Pvc Panels")]
        PvcPanels = 15,

        [Display(Name = "Alu Panels")]
        AluPanels = 16,

        [Display(Name = "Cassoneto")]
        Cassoneto = 17,

        [Display(Name = "Internal Blinds")]
        InternalBlinds = 18,

        [Display(Name = "Pergola")]
        Pergola = 19
    }

    public enum Brand
    {
        [Display(Name = "AliPlast")]
        AliPlast = 0,

        [Display(Name = "AluPlast")]
        AluPlast = 1,

        [Display(Name = "AluProf")]
        AluProf = 2,

        [Display(Name = "EkoSun")]
        EkoSun = 3,

        [Display(Name = "Gealan")]
        Gealan = 4,

        [Display(Name = "Deceuninck")]
        Deceuninck = 5,

        [Display(Name = "Salamander")]
        Salamander = 6,

        [Display(Name = "Reynaers")]
        Reynaers = 7,

        [Display(Name = "EkoOkna")]
        EkoOkna = 8,

        [Display(Name = "Forster")]
        Forster = 9
    }
}