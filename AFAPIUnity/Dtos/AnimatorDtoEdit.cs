using APAF.Domain.Core.Entities.AssetBundel_Animator;
using APAF.Domain.Core.Enums;
using System.ComponentModel.DataAnnotations;
namespace AFAPIUnity.Dtos
{
    public class AnimatorDtoEdit
    {
        [Required]
        public long Id { get; set; }
        [Required]
        [MaxLength(90, ErrorMessage = "Charachters is not bigger than 90")]
        [MinLength(5, ErrorMessage = "Charachters is not smaller than 5")]
        public string FullName { get; set; }
        [Required]
        public string version { get; set; }
        [Required]
        [MaxLength(11, ErrorMessage = "Charachters is not bigger than 11")]
        [MinLength(11, ErrorMessage = "Charachters is not smaller than 11")]
        //mobliNumbervalidation
        public string PhoneNumber { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public bool DoesItRemove { get; set; }
        [Required]
        public int TotalScore { get; set; }
        [Required]
        public int Score { get; set; }
        [Required]
        public long AvatarId { get; set; }
    }
}
