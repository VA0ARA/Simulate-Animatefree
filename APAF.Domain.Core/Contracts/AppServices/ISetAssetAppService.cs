using APAF.Domain.Core.Dtos.AssetBundles;
using APAF.Domain.Core.Dtos.Avatars;

namespace APAF.Domain.Core.Contracts.AppServices
{
    public interface ISetAssetAppService
    {
        Task<AssetBundelDTos> SetAssetById(long Id, CancellationToken cancellationToken);
        Task<List<AssetBundelDTos>> SetAllAsset(CancellationToken cancellationToken);
        Task<List<AvatarDtos>> SetDefaulAvatar(CancellationToken cancellationToken);
        Task<AvatarDtos> SetAvatarById(long Id, CancellationToken cancellationToken);
    }
}
