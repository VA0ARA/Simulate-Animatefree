using APAF.AppServices.Domain;
using APAF.Domain.Core.Contracts.AppServices;
using APAF.Domain.Core.Dtos.Admin;
using APAF.Domain.Core.Dtos.Matchs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AFAP.EndPointMVCPattern.Controllers.Match
{
    [Authorize(Roles = "Owner ,Admin")]
    public class MatchController : Controller
    {
        #region Property-Constructore
        private readonly IMatchAppService _matchRepositoryAppService;

        public MatchController(IMatchAppService matchRepositoryAppService)
        {
            _matchRepositoryAppService = matchRepositoryAppService;
        }
        #endregion
        #region MatchShowDetailPanel
        [HttpGet]
        public async Task<IActionResult> MatchShowDetailPanel(CancellationToken cancellationToken)
        {
            var ListOfMatchs = await _matchRepositoryAppService.GetAll(cancellationToken);
            return View(ListOfMatchs);
        }
        #endregion
        #region Create
        [HttpGet]
        public IActionResult CreateMatch()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateMatch(MatchDto matchDto, CancellationToken cancellationToken)
        {
            await _matchRepositoryAppService.Create(matchDto.Name,matchDto.Score,matchDto.Type,matchDto.AssetBundlesId,matchDto.DoesItRemove, cancellationToken);
            return RedirectToAction("MatchShowDetailPanel");
        }
        #endregion
        #region Edit
        [HttpGet]
        public async Task<IActionResult> EditMatch(long Id, CancellationToken cancellationToken)
        {

            var MatchDto = await _matchRepositoryAppService.GetById(Id, cancellationToken);
            if (MatchDto != null)
            {
                return View(MatchDto);
            }
            else
            {
                return RedirectToAction("EditMatch");
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditMatch(MatchDto objDto, CancellationToken cancellationToken)
        {
            if (objDto != null)
            {
                await _matchRepositoryAppService.Edit(objDto.Id, objDto.Name,objDto.Score,objDto.Type,objDto.AssetBundlesId,objDto.DoesItRemove, cancellationToken);
                return RedirectToAction("GetAllMatch");
            }
            else
            {
                return RedirectToAction("EditMatch");
            }
        }
        #endregion
        #region Delete
   /*     [HttpPost]
        public async Task<IActionResult> DeleteAdmin(AdminDto objDto, CancellationToken cancellationToken)
        {
            if (objDto != null)
            {
                await _adminRepositoryAppService.Remove(objDto.Id, cancellationToken);
                return RedirectToAction("GetAllAdmin");
            }
            else
            {
                return RedirectToAction("EditAdmin");
            }

        }*/
        #endregion
    }
}
