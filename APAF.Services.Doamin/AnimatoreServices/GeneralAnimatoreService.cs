using System;
using APAF.Domain.Core.Contracts.Repository.Animators;
using APAF.Domain.Core.Contracts.Services.AnimatoreServics;
using APAF.Domain.Core.Dtos.Animators;

namespace APAF.Services.Domain.AnimatoreServices;

public class GeneralAnimatoreService:IGeneralAnimatoreService
{
    #region Propery
        private readonly IGeneralAnimRepository _GeneralAnimRepo;
         private   GeneralAnimatoreService ( IGeneralAnimRepository generalAnimRepo){
            _GeneralAnimRepo = generalAnimRepo;
         }
            
    #endregion
    #region  behavior
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
