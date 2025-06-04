using AFAP.EndPointMVCPattern.CustomValidators;
using APAF.Domain.Core.Contract.AppServices;
using APAF.Domain.Core.Dtos.Admin;
using APAF.Domain.Core.Dtos.APK;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace AFAP.EndPointMVCPattern.Controllers.APK
{
    [Authorize(Roles = "Owner ,Admin")]
    public class APKController : Controller
    {
        #region Property-Constructore
        private readonly IAPKAppService _aPKRepositoryAppService;
        private StringValidation varstring = new StringValidation();

        public APKController(IAPKAppService aPKRepositoryAppService)
        {
            _aPKRepositoryAppService = aPKRepositoryAppService;
        }
        #endregion
        #region Create
        //  IPServer/Admin/AdminLogIn
        [HttpGet]
        public IActionResult CreateAPK()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAPK(APKDto apkDto, CancellationToken cancellationToken)
        {
            #region Check string validation
            if (varstring.Verifystring(apkDto.Description) == true)
            {
                ModelState.AddModelError("توضیحات", " لطفا توضیحات را  حروف وارد کنید");
            }
            #endregion
            #region check database attribute validation
            if (ModelState.IsValid && apkDto != null)
            {
                await _aPKRepositoryAppService.Create(apkDto.VersionCode, apkDto.Description, apkDto.DoesItRemove, cancellationToken);
                TempData["success"] = "اطلاعات با موفقیت ثبت شد";
                return RedirectToAction("APKShowDetailPanel");
            }
            else
            {
                TempData["error"] = "اطلاعات ثبت نشد دوباره سعی کنید";
            }
            #endregion
            return View();
        }
        #endregion
        #region APKShowDetailPanel
        [HttpGet]
        public async Task<IActionResult> APKShowDetailPanel(CancellationToken cancellationToken)
        {
            var ListOfApk = await _aPKRepositoryAppService.GetAll(cancellationToken);
            return View(ListOfApk);
        }
        #endregion
        #region Edit
        [HttpGet]
        public async Task<IActionResult> EditAPK(long Id, CancellationToken cancellationToken)
        {

            var ApkDto = await _aPKRepositoryAppService.GetById(Id, cancellationToken);
            if (ApkDto != null)
            {
                return View(ApkDto);
            }
            else
            {
                return RedirectToAction("EditAPK");
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditAPK(APKDto objDto, CancellationToken cancellationToken)
        {
            #region Check string validation
            if (varstring.Verifystring(objDto.Description) == true)
            {
                ModelState.AddModelError("توضیحات", " لطفا توضیحات را  حروف وارد کنید");
            }
            #endregion
            #region check database attribute validation
            if (ModelState.IsValid && objDto != null)
            {
                await _aPKRepositoryAppService.Edit(objDto.Id, objDto.VersionCode ,objDto.Description, objDto.DoesItRemove,cancellationToken);
                TempData["success"] = "اطلاعات با موفقیت ثبت شد";
                return RedirectToAction("APKShowDetailPanel");
            }
            else
            {
                TempData["error"] = "اطلاعات ثبت نشد دوباره سعی کنید";
            }
            #endregion
            return View();
        }
        #endregion
        #region Delete
        [HttpPost]
        public async Task<IActionResult> DeleteAPk(APKDto objDto, CancellationToken cancellationToken)
        {
            if (objDto != null)
            {
                await _aPKRepositoryAppService.Remove(objDto.Id, cancellationToken);
                return RedirectToAction("GetAllAPK");
            }
            else
            {
                return RedirectToAction("EditAPK");
            }

        }
        #endregion
    }
    
}
