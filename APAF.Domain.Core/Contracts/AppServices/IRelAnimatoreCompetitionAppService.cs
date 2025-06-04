using System;
using APAF.Domain.Core.Dtos.RelAnimtoreCompetitions;


namespace APAF.Domain.Core.Contracts.AppServices;

public interface IRelAnimatoreCompetitionAppService
{
        Task Create(RelAnimatoreCompetitionDto ObjRelAnimCompDto,CancellationToken cancellationToken);
        Task Edit(int id,CancellationToken cancellationToken);
        Task Remove(int id, CancellationToken cancellationToken);
        Task<List<RelAnimatoreCompetitionDto>> GetAll(CancellationToken cancellationToken);
        Task<RelAnimatoreCompetitionDto> GetById(int id, CancellationToken cancellationToken);

}
