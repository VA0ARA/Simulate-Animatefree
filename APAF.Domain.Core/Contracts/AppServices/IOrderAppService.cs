using System;
using APAF.Domain.Core.Dtos.Orders;

namespace APAF.Domain.Core.Contracts.AppServices;

public interface IOrderAppService
{
        Task Create(OrderDto ObjOrderDto,CancellationToken cancellationToken);
        Task Edit(int id,CancellationToken cancellationToken);
        Task Remove(int id, CancellationToken cancellationToken);
        Task<List<OrderDto>> GetAll(CancellationToken cancellationToken);
        Task<OrderDto> GetById(int id, CancellationToken cancellationToken);

}
