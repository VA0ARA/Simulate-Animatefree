using System;
using APAF.Domain.Core.Dtos.Animators;

namespace APAF.Domain.Core.Contracts.Repository.Animators;

public interface IUnityCompetionRepository
{
        Task UpdateDetailAnimForCompetition(long id,CompetitionDetailAnimDto obj,CancellationToken cancellationToken);
        Task EditDetailAnim(SignInDetailAnimDto obj,CancellationToken cancellationToken);
}
