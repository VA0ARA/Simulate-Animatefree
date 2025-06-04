using APAF.Domain.Core.Dtos.Admin;
using APAF.Domain.Core.Dtos.Owner;
using APAF.Domain.Core.Enums;
namespace APAF.Domain.Core.Contracts.AppServices
{
    public interface IOwnerAppService
    {
        Task Create(string fullname, string password, string username, bool doesitremove, Roll roll, CancellationToken cancellationToken);
        Task Edit(long id, string fullname, string password, string username, bool doesitremove, CancellationToken cancellationToken);
        Task Remove(long id, CancellationToken cancellationToken);
        Task<List<OwnerDto>> GetAll(CancellationToken cancellationToken);
    }
}
