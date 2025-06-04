using APAF.Domain.Core.Dtos.Admin;
using APAF.Domain.Core.Dtos.AssetBundles;
using APAF.Domain.Core.Enums;
namespace APAF.Domain.Core.Contracts.AppServices
{
    public interface IAssetBundleAppService
    {
        Task Create(AssetBundelDTos ObjAssetBundelDto, CancellationToken cancellationToken);
        Task Edit(int Id, CancellationToken cancellationToken);
        Task Remove(int Id, CancellationToken cancellationToken);
        Task<List<AssetBundelDTos>> GetAll(CancellationToken cancellationToken);
        Task<AssetBundelDTos> GetById(int Id, CancellationToken cancellationToken);
    }
}
