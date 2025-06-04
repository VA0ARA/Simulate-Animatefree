using APAF.Domain.Core.Dtos.Animators;
using APAF.Domain.Core.Dtos.CompetitionDtos;
namespace APAF.Domain.Core.Contracts.AppServices
{
    public  interface IAnimatorAppService
    {
        // This is one of  Entity,I want to show detail for instance 
        //AvatarId=1 in Sql for Defual State
        #region  Just for Unity User
        Task CreatePrimitiveDetailAnim(PrimitiveDetailAnimDto obj,CancellationToken cancellationToken);
        Task EditPrimitiveDetailAnim(PrimitiveDetailAnimDto  obj,CancellationToken cancellationToken);
        Task<List<PrimitiveDetailAnimDto>> GetAllPrimitiveDetailAnim(CancellationToken cancellationToken);
        Task<List<PrimitiveDetailAnimDto>> GetPrimitiveDetailAnimById(long Id,CancellationToken cancellationToken);
        #endregion
        #region for unity user who takepart in competition
        Task CreateCompetitionDetailAnim(CompetitionDetailAnimDto obj,CancellationToken cancellationToken);
        Task EditDetailAnim(SignInDetailAnimDto obj,CancellationToken cancellationToken);
        #endregion
        #region for create Form for scratch
        Task CreateFormSignInAnim(SignInDetailAnimDto obj,CancellationToken cancellationToken);
        #endregion
        #region  GeneralFor all sections
        Task<PrimitiveDetailAnimDto> GetByPhone(string phone, CancellationToken cancellationToken);
        Task RemoveAnim(long id, CancellationToken cancellationToken);
        #endregion
        #region Profile
        // Task<List<GiveScore>> GetProfileById(long Id,CancellationToken cancellationToken);
        #endregion

    }
}
