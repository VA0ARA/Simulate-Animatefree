using System.ComponentModel.DataAnnotations;

namespace APAF.Domain.Core.Enums
{
    public enum LevelOfStudy
    {
        [Display(Name = "اول")]
        first = 1,

        [Display(Name = "دوم")]
        second = 2,

        [Display(Name = "سوم")]
        third = 3,

        [Display(Name = "چهارم")]
        fourth = 4,

        [Display(Name = "پنجم")]
        fifth = 5,

        [Display(Name = "ششم")]
        sixth = 6,

        [Display(Name = "هفتم")]
        seventh = 7,

        [Display(Name = "هشتم")]
        eight = 8,

        [Display(Name = "نهم")]
        ninth = 9,

        [Display(Name = "دهم")]
        tenth = 10,

        [Display(Name = "یازدهم")]
        elevennth = 11,

        [Display(Name = "دوازدهم")]
        twelfth = 12
    }
}
