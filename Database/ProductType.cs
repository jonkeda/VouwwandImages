using System.ComponentModel.DataAnnotations;

namespace VouwwandImages.Database;

public enum ProductType
{
    [Display(Name = "None")]
    None = 0,

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
    Pergola = 19,

    [Display(Name = "Windows")]
    Windows = 20,

}