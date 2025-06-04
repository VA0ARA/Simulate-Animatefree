using System;
using APAF.Domain.Core.Dtos.BannerOfAsset;

namespace APAF.Domain.Core.Contracts.AppServices;

public interface IBannerOfAssetAppService
{
        Task Create(BannerOfAssetDto ObjBannerOfAssetDto,CancellationToken cancellationToken);
        Task Edit(int id,CancellationToken cancellationToken);
        Task Remove(int id, CancellationToken cancellationToken);
        Task<List<BannerOfAssetDto>> GetAll(CancellationToken cancellationToken);
        Task<BannerOfAssetDto> GetById(int id, CancellationToken cancellationToken);

}
