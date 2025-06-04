
using APAF.Domain.Core.Dtos.Animators;
using APAF.Domain.Core.Dtos.CompetitionDtos;
namespace APAF.Domain.Core.Contracts.Repository.Animators
{
    public interface IGeneralAnimRepository
    {
        //AvatarId=1 in Sql for Defual State
        Task<long> DoesThisAnimatoreExsitByPhone(string phone, CancellationToken cancellationToken);
        Task<PrimitiveDetailAnimDto> GetByPhone(string phone, CancellationToken cancellationToken);
        Task RemoveAnim(long id, CancellationToken cancellationToken);
        Task<List<PrimitiveDetailAnimDto>> GetAllPrimitiveDetailAnim(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<PrimitiveDetailAnimDto>GetPrimitiveDetailAnimById(long Id,CancellationToken cancellationToken);
    }
}
