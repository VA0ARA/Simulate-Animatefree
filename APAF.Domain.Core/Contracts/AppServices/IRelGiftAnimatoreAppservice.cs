using System;
using APAF.Domain.Core.Dtos.RelGitAnims;

namespace APAF.Domain.Core.Contracts.AppServices;

public interface IRelGiftAnimatoreAppservice
{
        Task Create(RelGiftAnimatoreDto ObjGiftAnim,CancellationToken cancellationToken);
        Task Edit(int id,CancellationToken cancellationToken);
        Task Remove(int id, CancellationToken cancellationToken);
        Task<List<RelGiftAnimatoreDto>> GetAll(CancellationToken cancellationToken);
        Task<RelGiftAnimatoreDto> GetById(int id, CancellationToken cancellationToken);

}
