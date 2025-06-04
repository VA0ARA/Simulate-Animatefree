using System;
using APAF.Domain.Core.Dtos.Animators;

namespace APAF.Domain.Core.Contracts.Services.AnimatoreServics;

public interface IGeneralAnimatoreService
{
        Task<long> DoesThisAnimatoreExsitByPhone(string phone, CancellationToken cancellationToken);
        Task<PrimitiveDetailAnimDto> GetByPhone(string phone, CancellationToken cancellationToken);
        Task RemoveAnim(long id, CancellationToken cancellationToken);
        Task<List<PrimitiveDetailAnimDto>> GetAllPrimitiveDetailAnim(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<PrimitiveDetailAnimDto>GetPrimitiveDetailAnimById(long Id,CancellationToken cancellationToken);

}
