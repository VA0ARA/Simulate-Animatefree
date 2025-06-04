using System;
using APAF.Domain.Core.Dtos.Animators;

namespace APAF.Domain.Core.Contracts.Services;

public interface IProfileOfAnimatoreService
{
        //persoanl detail
        Task<AnimatoreProfileDto> GetAnimatoreWithSchool(long animatoreId, CancellationToken cancellationToken);

    //competion -score- gift
        Task<List<AnimatoreGiftCometionDto>> GetAnimatoreCompetitionsAndGifts(long id,CancellationToken cancellationToken);
    //shopasset

}
