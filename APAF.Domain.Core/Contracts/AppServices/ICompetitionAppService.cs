using APAF.Domain.Core.Dtos.Competitions;
using APAF.Domain.Core.Enums;
namespace APAF.Domain.Core.Contracts.AppServices
{
    public  interface ICompetitionAppService
    {
        Task Create(CompetitionDto ObjCopetitionDto,CancellationToken cancellationToken);
        Task Edit(int id,CancellationToken cancellationToken);
        Task Remove(int id, CancellationToken cancellationToken);
        Task<List<CompetitionDto>> GetAll(CancellationToken cancellationToken);
        Task<CompetitionDto> GetById(int id, CancellationToken cancellationToken);

    }
}
