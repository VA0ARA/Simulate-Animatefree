using APAF.Domain.Core.Dtos.Animators;
using APAF.Domain.Core.Dtos.CompetitionDtos;
namespace APAF.Domain.Core.Contracts.Services.AnimatoreServics
{
    public  interface IAnimatorUnityService:IGeneralAnimatoreService
    {
        Task CreatePrimitiveDetailAnim(PrimitiveDetailAnimDto obj,CancellationToken cancellationToken);
        Task EditPrimitiveDetailAnim(PrimitiveDetailAnimDto obj,CancellationToken cancellationToken);

    }
}
