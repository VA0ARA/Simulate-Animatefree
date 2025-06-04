using APAF.Domain.Core.Dtos.Cartoons;
namespace APAF.Domain.Core.Contracts.AppServices
{
    public interface ICartoonAppService
    {
        Task Create(CartoonDtos ObjCartoonDto,CancellationToken cancellationToken);
        Task Edit(int id,CancellationToken cancellationToken);
        Task Remove(int id, CancellationToken cancellationToken);
        Task<List<CartoonDtos>> GetAll(CancellationToken cancellationToken);
        Task<CartoonDtos> GetById(int id, CancellationToken cancellationToken);
    }
}
