using System.ComponentModel.DataAnnotations;
namespace APAF.Domain.Core.Enums
{
    public enum LevelOfSchool
    {
        [Display(Name = "ابتدایی اول")]
        EbtedaiAval = 1,
        
        [Display(Name = "ابتدایی دوم")]
        EbtedaiDovum = 2,
        
        [Display(Name = "متوسطه اول")]
        MotevaseteAval = 3,
        
        [Display(Name = "متوسطه دوم")]
        MotevaseteDovum = 4
    }
}

