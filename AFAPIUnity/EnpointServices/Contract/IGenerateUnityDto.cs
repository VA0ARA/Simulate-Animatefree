using APAF.Domain.Core.Dtos.Animators;
using APAF.Domain.Core.Dtos.AssetBundles;
using APAF.Domain.Core.Dtos.Avatars;

namespace AFAPIUnity.EnpointServices.Contract
{
    public interface IGenerateUnityDto
    {
        Task <AnimatorDTos> GenerateAnimatore(long id,CancellationToken cancellationToken);
        List<AvatarDtos> GenerateAvatar(List<AvatarDtos> Avatars);
        List<AssetBundelDTos> GenerateAssetbunderl(List<AssetBundelDTos> Assets);
        Task<AvatarDtos> GenerateAvatarById(long id, CancellationToken cancellationToken);
    }
}
