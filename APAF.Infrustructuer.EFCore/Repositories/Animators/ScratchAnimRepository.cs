using System;
using APAF.Domain.Core.Contracts.Repository.Animators;
using APAF.Domain.Core.Dtos.Animators;
using APAF.Domain.Core.Entities.Animators;
using APAF.Infrastructure.EFCore.Common;
using Microsoft.EntityFrameworkCore;

namespace APAF.Infrastructure.EFCore.Repositories.Animators;

public class ScratchAnimRepository:IScratchAnimRepository
{
        #region Property
        private readonly AppDbContext _appDbContext;

        public ScratchAnimRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        #endregion
        #region for create Form for scratch
        public async  Task CreateFormSignInAnim(SignInDetailAnimDto obj, CancellationToken cancellationToken)
        {
            var ObjAnim = new Animatore();
            ObjAnim .Name=obj.Name;
            ObjAnim.LastName=obj.LastName;
            ObjAnim.PhoneNumber = obj.PhoneNumber;
            ObjAnim.Register=obj.Register;
            ObjAnim.gender=obj.gender;
            ObjAnim.ModelOfPhone=obj.ModelOfPhone;
            ObjAnim.FkIdversionOfAPK=obj.IdApk;
            ObjAnim.FkAvatar=obj.AvatarId;
            ObjAnim.DoesItremove=obj.DoesItRemove;
            ObjAnim.BrithDay=obj.BrithDay;
            ObjAnim.levelOfStudy=ObjAnim.levelOfStudy;
            ObjAnim.Province=obj.provinces;
            ObjAnim.City=obj.city;
            ObjAnim.Address=obj.Address;

            await _appDbContext.Animatores.AddAsync(ObjAnim , cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task EditDetailAnim(SignInDetailAnimDto obj,CancellationToken cancellationToken){
                    var entity = await _appDbContext.Animatores
                .FirstOrDefaultAsync(x => x.Id == obj.Id, cancellationToken);
            if (entity != null)
            {
            entity.Id = obj.Id;
            entity .Name=obj.Name;
            entity.LastName=obj.LastName;
            entity.PhoneNumber = obj.PhoneNumber;
            entity.Register=obj.Register;
            entity.gender=obj.gender;
            entity.ModelOfPhone=obj.ModelOfPhone;
            entity.ModelOfPhone=obj.ModelOfPhone;
            entity.BrithDay=obj.BrithDay;
            entity.DoesItremove=obj.DoesItRemove;
            entity.levelOfStudy=obj.levelOfStudy;
            entity.Province=obj.provinces;
            entity.City=obj.city;
            entity.FKIDSchool=obj.SchoolId;
                await _appDbContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new Exception($"Animatore with id not found");
            } 
        }
        #endregion
}
