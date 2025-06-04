using System;
using APAF.Domain.Core.Dtos.Animators;

namespace APAF.Domain.Core.Contracts.Repository.Animators;

public interface IScratchAnimRepository
{
        Task CreateFormSignInAnim(SignInDetailAnimDto obj,CancellationToken cancellationToken);
        Task EditDetailAnim(SignInDetailAnimDto obj,CancellationToken cancellationToken);

}
