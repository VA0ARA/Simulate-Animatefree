using APAF.Domain.Core.Contracts.Repository;
using APAF.Domain.Core.Contracts.Repository.Animators;
using APAF.Domain.Core.Dtos.Animators;
using APAF.Domain.Core.Dtos.CompetitionDtos;
using APAF.Domain.Core.Entities.Animators;
using APAF.Domain.Core.Enums;
using APAF.Infrastructure.EFCore.Common;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
namespace APAF.Infrastructure.EFCore.Repositories
{
    public class GeneralAnimRepository : IGeneralAnimRepository
    {
        #region Property
        private readonly AppDbContext _appDbContext;

        public GeneralAnimRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        #endregion
        #region CRUD Opretion

       #region General For all sections
        public async Task<PrimitiveDetailAnimDto> GetByPhone(string phone, CancellationToken cancellationToken)
         {
                        var entity = await _appDbContext.Animatores
                  .FirstOrDefaultAsync(x => x.PhoneNumber == phone, cancellationToken);
            if (entity != null)
            {
                var Dtoentity = new PrimitiveDetailAnimDto();
                Dtoentity.Id = entity.Id;
                Dtoentity.Name = entity.Name;
                Dtoentity.PhoneNumber = entity.PhoneNumber;
                Dtoentity.DoesItRemove = entity.DoesItremove;
                Dtoentity.Register = entity.Register;
                Dtoentity.gender = entity.gender;
                Dtoentity.FkIdversionOfAPK=entity.FkIdversionOfAPK;
                Dtoentity.FkAvatar = entity.FkAvatar;
                return Dtoentity;
            }
            else
            {
                throw new Exception($"Animatore with number  not found");
            }
              
         }
         public async Task<long> DoesThisAnimatoreExsitByPhone(string phone, CancellationToken cancellationToken)
         {
                        var entity = await _appDbContext.Animatores
                  .FirstOrDefaultAsync(x => x.PhoneNumber == phone, cancellationToken);
            if (entity != null)
            {
                return entity.Id;
            }
            else
            {
                throw new Exception($"Animatore with number  not found");
            }
              
         }
        public async Task RemoveAnim(long id, CancellationToken cancellationToken)
        {
                       var entity = await _appDbContext.Animatores
                  .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (entity != null)
            {
                _appDbContext.Animatores.Remove(entity);
                await _appDbContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new Exception($"Animatore with id  not found");
            }
        }
        public async  Task<List<PrimitiveDetailAnimDto>> GetAllPrimitiveDetailAnim(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
                var result = await _appDbContext.Animatores
                    .Skip((pageNumber - 1) * pageSize)  // محاسبه و اسکیپ کردن رکوردهای قبلی بر اساس شماره صفحه
                    .Take(pageSize)                    // تعداد رکوردهای هر صفحه
                    .Select(x => new PrimitiveDetailAnimDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        PhoneNumber = x.PhoneNumber,
                        DoesItRemove = x.DoesItremove,
                        Register = x.Register,  
                        gender=x.gender,
                        ModelOfPhone=x.ModelOfPhone,
                        FkIdversionOfAPK=x.FkIdversionOfAPK,
                        FkAvatar=x.FkAvatar  
                    }).ToListAsync(cancellationToken);

    if (result != null && result.Count > 0)
    {
        return result;
    }
    else
    {
         throw new Exception($"No Animators found on page {pageNumber}.");
    }
        }
        public async  Task<PrimitiveDetailAnimDto> GetPrimitiveDetailAnimById(long Id, CancellationToken cancellationToken)
        {
                var entity = await _appDbContext.Animatores
                                 .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
            if (entity != null)
            {
                var Dtoentity = new PrimitiveDetailAnimDto();
                Dtoentity.Id = entity.Id;
                Dtoentity.Name = entity.Name;
                Dtoentity.PhoneNumber = entity.PhoneNumber;
                Dtoentity.DoesItRemove = entity.DoesItremove;
                Dtoentity.Register = entity.Register;
                Dtoentity.gender = entity.gender;
                Dtoentity.FkIdversionOfAPK=entity.FkIdversionOfAPK;
                Dtoentity.FkAvatar = entity.FkAvatar;
                return Dtoentity;
            }
            else
            {
                throw new Exception($"Animatore with id  not found");
            }
        }
       #endregion


        #endregion
    }
}

