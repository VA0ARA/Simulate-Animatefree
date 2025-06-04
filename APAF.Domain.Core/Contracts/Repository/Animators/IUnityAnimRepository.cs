using System;
using APAF.Domain.Core.Dtos.Animators;

namespace APAF.Domain.Core.Contracts.Repository.Animators;

public interface IUnityAnimRepository
{
        Task CreatePrimitiveDetailAnim(PrimitiveDetailAnimDto obj,CancellationToken cancellationToken);
        Task EditPrimitiveDetailAnim(PrimitiveDetailAnimDto obj,CancellationToken cancellationToken);
}
