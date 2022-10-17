using System.ComponentModel.DataAnnotations;

namespace VouwwandImages.Database;

public enum Brand
{
    [Display(Name = "None")]
    None = 0,

    [Display(Name = "AliPlast")]
    AliPlast = 1,

    [Display(Name = "AluPlast")]
    AluPlast = 2,

    [Display(Name = "AluProf")]
    AluProf = 3,

    [Display(Name = "EkoSun")]
    EkoSun = 4,

    [Display(Name = "Gealan")]
    Gealan = 5,

    [Display(Name = "Deceuninck")]
    Deceuninck = 6,

    [Display(Name = "Salamander")]
    Salamander = 7,

    [Display(Name = "Reynaers")]
    Reynaers = 8,

    [Display(Name = "EkoOkna")]
    EkoOkna = 9,

    [Display(Name = "Forster")]
    Forster = 10
}