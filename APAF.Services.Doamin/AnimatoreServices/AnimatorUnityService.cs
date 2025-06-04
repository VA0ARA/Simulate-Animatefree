
using APAF.Domain.Core.Contracts.Repository;
using APAF.Domain.Core.Contracts.Repository.Animators;
using APAF.Domain.Core.Contracts.Services;
using APAF.Domain.Core.Contracts.Services.AnimatoreServics;
using APAF.Domain.Core.Dtos.Animators;
using APAF.Domain.Core.Dtos.CompetitionDtos;
namespace APAF.Services.Domain.AnimatoreServices
{
    public class AnimatorUnityService : IAnimatorUnityService
    {
        #region Propery
        private readonly IGeneralAnimRepository _GeneralAnimRepo;
        private readonly IUnityAnimRepository _UnityAnimRepo;
        public  AnimatorUnityService ( IGeneralAnimRepository generalAnimRepo,IUnityAnimRepository unityAnimRepo){
            _GeneralAnimRepo = generalAnimRepo;
            _UnityAnimRepo = unityAnimRepo;
        }
        #endregion
        #region Behavior
        public async Task CreatePrimitiveDetailAnim(PrimitiveDetailAnimDto obj, CancellationToken cancellationToken)
        
            =>await _UnityAnimRepo.CreatePrimitiveDetailAnim(obj, cancellationToken);
        public async Task EditPrimitiveDetailAnim(PrimitiveDetailAnimDto obj, CancellationToken cancellationToken)
        
            => await  _UnityAnimRepo.EditPrimitiveDetailAnim(obj,cancellationToken);
        
        public async Task<long> DoesThisAnimatoreExsitByPhone(string phone, CancellationToken cancellationToken)
        
            => await  _GeneralAnimRepo.DoesThisAnimatoreExsitByPhone(phone,cancellationToken);

        public async Task<List<PrimitiveDetailAnimDto>> GetAllPrimitiveDetailAnim(int pageNumber, int pageSize, CancellationToken cancellationToken)
         => await  _GeneralAnimRepo.GetAllPrimitiveDetailAnim(pageNumber,pageSize,cancellationToken); 

        public async Task<PrimitiveDetailAnimDto> GetByPhone(string phone, CancellationToken cancellationToken)
        => await _GeneralAnimRepo.GetByPhone(phone,cancellationToken);

        public async Task<PrimitiveDetailAnimDto> GetPrimitiveDetailAnimById(long Id, CancellationToken cancellationToken)
        =>await _GeneralAnimRepo.GetPrimitiveDetailAnimById(Id,cancellationToken);

        public async Task RemoveAnim(long id, CancellationToken cancellationToken)
        => await _GeneralAnimRepo.RemoveAnim(id,cancellationToken);
        #endregion
    }   
}

  
