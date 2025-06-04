using System;
using APAF.Domain.Core.Contracts.Repository.Animators;
using APAF.Domain.Core.Dtos.Animators;
using APAF.Infrastructure.EFCore.Common;
using Microsoft.EntityFrameworkCore;

namespace APAF.Infrastructure.EFCore.Repositories.Animators;

public class UnityCompetionRepository:IUnityCompetionRepository
{
        #region Property
        private readonly AppDbContext _appDbContext;

        public UnityCompetionRepository (AppDbContext appDbContext)
       {
            _appDbContext = appDbContext;
        }
        #endregion
        #region for unity user who takepart in competition
        //get id from DoesThisAnimatoreExsitByPhone method
        public async Task UpdateDetailAnimForCompetition(long id,CompetitionDetailAnimDto obj,CancellationToken cancellationToken){
                        var entity = await _appDbContext.Animatores
                  .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (entity != null)
            {
            entity .LastName=obj.LastName;
            entity.BrithDay = obj.BrithDay;
            entity.levelOfStudy=obj.levelOfStudy;
            entity.Province=obj.provinces;
            entity.City=obj.city;
            entity.Address=obj.Address;
            entity.FKIDSchool=obj.SchoolId;
            //await _appDbContext.Animatores.AddAsync(ObjAnim , cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            }

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
