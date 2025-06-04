using System;
using APAF.Domain.Core.Contracts.Repository.Animators;
using APAF.Domain.Core.Contracts.Services.AnimatoreServics;
using APAF.Domain.Core.Dtos.Animators;

namespace APAF.Services.Domain.AnimatoreServices;

public class AnimatoreScratchServise : IAnimatoreScratchServise
{
        #region Propery
        private readonly IGeneralAnimRepository _GeneralAnimRepo;
        private readonly IScratchAnimRepository _ScratchAnimRepo;
        public  AnimatoreScratchServise ( IGeneralAnimRepository generalAnimRepo,IScratchAnimRepository scratchAnimRepo){
            _GeneralAnimRepo = generalAnimRepo;
            _ScratchAnimRepo = scratchAnimRepo;
        }
        #endregion
    #region  behavior
       public async Task CreateFormSignInAnim(SignInDetailAnimDto obj, CancellationToken cancellationToken)
         => await _ScratchAnimRepo.CreateFormSignInAnim(obj,cancellationToken);
       public async Task EditDetailAnim(SignInDetailAnimDto obj, CancellationToken cancellationToken)
        => await _ScratchAnimRepo.EditDetailAnim(obj,cancellationToken);
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
