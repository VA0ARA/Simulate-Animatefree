using APAF.AppServices.Domain;
using APAF.Domain.Core.Contracts.AppServices;
using Microsoft.AspNetCore.Mvc;

namespace AFAP.EndPointMVCPattern.Controllers.Cartoon
{
    public class CartoonController : Controller
    {
        /*        //just give Score for user 
                //we get video File from unity in API endpoint 
                #region Property-Constructore
                private readonly ICartoonRepositoryAppService _cartoonRepositoryAppService;
                private readonly ICalculateScoreAppService _calculateScoreAppService;
                public CartoonController(IAvatarRepositoryAppService avatarRepositoryAppService,  ICalculateScoreAppService calculateScoreAppService)
                {
                    _calculateScoreAppService = calculateScoreAppService;
                }
                #endregion
                #region CartoonShowDetailPanel
                [HttpGet]
                public async Task<IActionResult> CartoonShowDetailPanel(CancellationToken cancellationToken)
                {
                    var ListOfCarton = await _cartoonRepositoryAppService.GetAll(cancellationToken);
                    return View(ListOfCarton);
                }
                #endregion*/
    }
}
