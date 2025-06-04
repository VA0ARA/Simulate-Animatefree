using System;
using APAF.Domain.Core.Contracts.Repository.Animators;
using APAF.Domain.Core.Dtos.Animators;
using APAF.Domain.Core.Entities.Animators;
using APAF.Infrastructure.EFCore.Common;
using Microsoft.EntityFrameworkCore;

namespace APAF.Infrastructure.EFCore.Repositories.Animators;

public class UnityAnimRepository:IUnityAnimRepository
{
        #region Property
        private readonly AppDbContext _appDbContext;

        public UnityAnimRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        #endregion
        #region Just Unity-User
        public async  Task CreatePrimitiveDetailAnim(PrimitiveDetailAnimDto obj, CancellationToken cancellationToken)
        {
            var ObjAnim = new Animatore();
            ObjAnim .Name=obj.Name;
            ObjAnim.PhoneNumber = obj.PhoneNumber;
            ObjAnim.Register=obj.Register;
            ObjAnim.gender=obj.gender;
            ObjAnim.ModelOfPhone=obj.ModelOfPhone;
            ObjAnim.FkIdversionOfAPK=obj.FkIdversionOfAPK;
            ObjAnim.FkAvatar=obj.FkAvatar;
            ObjAnim.DoesItremove=obj.DoesItRemove;

            await _appDbContext.Animatores.AddAsync(ObjAnim , cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        public async  Task EditPrimitiveDetailAnim(PrimitiveDetailAnimDto obj, CancellationToken cancellationToken)
        {
                var entity = await _appDbContext.Animatores
                .FirstOrDefaultAsync(x => x.Id == obj.Id, cancellationToken);
            if (entity != null)
            {
            entity.Id = obj.Id;
            entity .Name=obj.Name;
            entity.PhoneNumber = obj.PhoneNumber;
            entity.Register=obj.Register;
            entity.gender=obj.gender;
            entity.ModelOfPhone=obj.ModelOfPhone;
            entity.FkIdversionOfAPK=obj.FkIdversionOfAPK;
            entity.FkAvatar=obj.FkAvatar;
            entity.DoesItremove=obj.DoesItRemove;

                await _appDbContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new Exception($"Animatore with id not found");
            }
        }
       #endregion

}
