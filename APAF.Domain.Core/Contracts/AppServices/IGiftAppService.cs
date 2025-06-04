using System;
using APAF.Domain.Core.Dtos.Competitions;
using APAF.Domain.Core.Dtos.Gifts;

namespace APAF.Domain.Core.Contracts.AppServices;

public interface IGiftAppService
{
        Task Create(GiftDto ObjGiftDto,CancellationToken cancellationToken);
        Task Edit(int id,CancellationToken cancellationToken);
        Task Remove(int id, CancellationToken cancellationToken);
        Task<List<GiftDto>> GetAll(CancellationToken cancellationToken);
        Task<GiftDto> GetById(int id, CancellationToken cancellationToken);


}
