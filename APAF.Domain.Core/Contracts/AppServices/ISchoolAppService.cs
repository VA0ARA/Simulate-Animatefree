using System;
using APAF.Domain.Core.Dtos.Schools;

namespace APAF.Domain.Core.Contracts.AppServices;

public interface ISchoolAppService
{
        Task Create(SchoolDto SchoolDto, CancellationToken cancellationToken);
        Task Edit(int Id, CancellationToken cancellationToken);
        Task Remove(int Id, CancellationToken cancellationToken);
        Task<List<SchoolDto>> GetAll(CancellationToken cancellationToken);
        Task<SchoolDto> GetById(int  id, CancellationToken cancellationToken);

}
