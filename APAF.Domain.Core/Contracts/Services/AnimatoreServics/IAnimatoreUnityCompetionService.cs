using System;
using APAF.Domain.Core.Dtos.Animators;

namespace APAF.Domain.Core.Contracts.Services.AnimatoreServics;

public interface IAnimatoreUnityCompetionService:IGeneralAnimatoreService
{
        Task UpdateDetailAnimForCompetition(long id,CompetitionDetailAnimDto obj,CancellationToken cancellationToken);
        Task EditDetailAnim(SignInDetailAnimDto obj,CancellationToken cancellationToken);

}
