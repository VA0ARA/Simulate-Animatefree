using APAF.Domain.Core.Contracts.AppServices;
using APAF.Domain.Core.Dtos.Animators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace AFAP.EndPointMVCPattern.Controllers.Animator
{
    //[Authorize(Roles = "Owner ,Admin")]
    public class AnimatorController : Controller
    {
        #region Property-Constructore
        private readonly IAnimatorAppService _animatoreRepositoryAppService;
        public AnimatorController(IAnimatorAppService animatoreRepositoryAppService)
        {
            _animatoreRepositoryAppService = animatoreRepositoryAppService;
        }
        #endregion
        #region AnimatorShowDetailPanel
        [HttpGet]
        public async Task<IActionResult> AnimatorShowDetailPanel(CancellationToken cancellationToken)
        {
            var ListOfAAnimator = await _animatoreRepositoryAppService.GetAll(cancellationToken);
            var ListOfActiveAdmin = ListOfAAnimator.Where(p => p.DoesItRemove == false).ToList();
            return View(ListOfAAnimator);
        }
        #endregion
        #region Edit
        [HttpGet]
        public async Task<IActionResult> EditAnimator(long Id, CancellationToken cancellationToken)
        {
            var AnimatorDto = await _animatoreRepositoryAppService.GetById(Id, cancellationToken);
            if (AnimatorDto != null)
            {
                return View(AnimatorDto);
            }
            else
            {
                return RedirectToAction("EditAnimator");
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditAnimator(AnimatorDTos objDto, CancellationToken cancellationToken)
        {
            if (objDto != null)
            {
                await _animatoreRepositoryAppService.Edit(objDto.Id, objDto.FullName, objDto.PhoneNumber, objDto.gender, objDto.DoesItRemove,objDto.VersionAPK,objDto.TotalScore,objDto.Score, objDto.AvatarId,cancellationToken);
                return RedirectToAction("AnimatorShowDetailPanel");
            }
            else
            {
                return RedirectToAction("EditAnimator");
            }
        }
        #endregion
    }
}
