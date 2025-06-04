using System;
using APAF.Domain.Core.Dtos.Animators;
using APAF.Domain.Core.Entities.Animators;
using APAF.Domain.Core.Enums;

namespace APAF.Domain.Core.Contracts.Repository;

public interface IAnimatoreFilterRepository
{
 Task<List<AnimatoreSearchBarDto>> GetAnimatorsByFilters(
    int? schoolId = null, 
    IranProvinces? province = null, 
    int? competitionId = null, 
    LevelOfStudy? levelOfStudy = null,
    int pageNumber = 1,   
    int pageSize = 10,   
    CancellationToken cancellationToken=default);
    Task<List<AnimatoreSearchBarDto>> GetTopAnimatorsByCompetitionScoreDescending(
    int competitionId, 
    int topCount, 
    int pageNumber = 1,   
    int pageSize = 10,   
    CancellationToken cancellationToken=default) ;

}
