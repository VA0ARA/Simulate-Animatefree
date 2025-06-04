using System;
using APAF.Domain.Core.Dtos.Animators;

namespace APAF.Domain.Core.Contracts.Services.AnimatoreServics;

public interface IAnimatoreScratchServise:IGeneralAnimatoreService
{

        Task CreateFormSignInAnim(SignInDetailAnimDto obj,CancellationToken cancellationToken);
        Task EditDetailAnim(SignInDetailAnimDto obj,CancellationToken cancellationToken);

}
