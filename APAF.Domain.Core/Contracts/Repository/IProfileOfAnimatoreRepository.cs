using System;
using APAF.Domain.Core.Dtos.Animators;

namespace APAF.Domain.Core.Contracts.Repository;

public interface IProfileOfAnimatoreRepository
{
    //persoanl detail
        Task<AnimatoreProfileDto> GetAnimatoreWithSchool(long animatoreId, CancellationToken cancellationToken);

    //competion -score- gift
        Task<List<AnimatoreGiftCometionDto>> GetAnimatoreCompetitionsAndGifts(long id,CancellationToken cancellationToken);
    //shopasset

}

