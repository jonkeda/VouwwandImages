using System.ComponentModel.DataAnnotations;
using VouwwandImages.Attributes;

namespace VouwwandImages.Database
{
    public enum Profile
    {

        None = 0,

        #region Cortizo

        [Brand(Brand.Cortizo)]
        [Display(Name = "Cor Vision +")]
        CorVisionPlus = 11001,

        #endregion

    }
}
