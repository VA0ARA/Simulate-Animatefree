using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APAF.Domain.Core.Contracts.AppServices
{
    public interface ICalculateScoreAppService
    {
        Task<int> MatchScore(long AnimatorId, long AssetbundleId, CancellationToken cancellationToken);
        Task<int> ClaculateTotalScore(int score, long AnimatorId, CancellationToken cancellationToken);
    }
}
