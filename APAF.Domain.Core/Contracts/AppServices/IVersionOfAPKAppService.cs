using APAF.Domain.Core.Dtos.APK;
namespace APAF.Domain.Core.Contract.AppServices
{
    public  interface IVersionOfAPKAppService
    {
        Task Create(APKDto apkDto, CancellationToken cancellationToken);
        Task Edit(int Id, CancellationToken cancellationToken);
        Task Remove(int Id, CancellationToken cancellationToken);
        Task<List<APKDto>> GetAll(CancellationToken cancellationToken);
        Task<APKDto> GetById(int  id, CancellationToken cancellationToken);
    }
}
