using APAF.Domain.Core.Dtos.Admin;
using APAF.Domain.Core.Dtos.Avatars;
namespace APAF.Domain.Core.Contracts.AppServices
{
    public interface IAvatarAppService
    {
        Task Create(AvatarDtos ObjAvatarDto,CancellationToken cancellationToken);
        Task Edit(int id,CancellationToken cancellationToken);
        Task Remove(int id, CancellationToken cancellationToken);
        Task<List<AvatarDtos>> GetAll(CancellationToken cancellationToken);
        Task<AvatarDtos> GetById(int id, CancellationToken cancellationToken);
    }
}
