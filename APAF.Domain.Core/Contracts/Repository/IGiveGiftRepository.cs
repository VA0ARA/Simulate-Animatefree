using System;

namespace APAF.Domain.Core.Contracts.Repository;

public interface IGiveGiftRepository
{
     Task GiveGiftToAnimator(long animatorId, int giftId, CancellationToken cancellationToken);


}
