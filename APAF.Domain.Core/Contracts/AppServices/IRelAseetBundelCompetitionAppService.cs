using System;
using APAF.Domain.Core.Dtos.RelAseetBundelCompetitions;

namespace APAF.Domain.Core.Contracts.AppServices;

public interface IRelAseetBundelCompetitionAppService
{
        Task Create(RelAssetBundelCompetitionDto ObjRelAssCompDto,CancellationToken cancellationToken);
        Task Edit(int id,CancellationToken cancellationToken);
        Task Remove(int id, CancellationToken cancellationToken);
        Task<List<RelAssetBundelCompetitionDto>> GetAll(CancellationToken cancellationToken);
        Task<RelAssetBundelCompetitionDto> GetById(int id, CancellationToken cancellationToken);

}
