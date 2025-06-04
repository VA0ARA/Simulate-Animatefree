using AFAP.EndPointMVCPattern.CustomValidators;
using APAF.Domain.Core.Contracts.AppServices;
using APAF.Domain.Core.Dtos.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace AFAP.EndPointMVCPattern.Controllers.Admin
{
    [Authorize(Roles = "Owner ,Admin")]
    public class AdminController : Controller
    {
        #region Property-Constructore
        private readonly IAdminAppService _adminRepositoryAppService;
        private StringValidation varstring = new StringValidation();
        private readonly ILogger<HomeController> _logger;
        public AdminController(IAdminAppService adminRepositoryAppService, ILogger<HomeController> logger)
        {
            _adminRepositoryAppService = adminRepositoryAppService;
            _logger = logger;
        }
        #endregion
        #region Create
        [HttpGet]
        public IActionResult CreateAdmin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAdmin(AdminDto adminoDto, CancellationToken cancellationToken)
        {
        #region Check string validation
            if (varstring.Verifystring(adminoDto.FullName) == true )
            {
                ModelState.AddModelError("نام و نام خانوادگی", " لطفانام و نام خانوادگی حروف وارد کنید");
            }
            if (varstring.Verifystring(adminoDto.UserName) == true)
            {
                ModelState.AddModelError("نام کاربری", " لطفا نام کاربری حروف وارد کنید");
            }
            #endregion
        #region check database attribute validation
            if (ModelState.IsValid && adminoDto != null)
            {
                await _adminRepositoryAppService.Create(adminoDto.FullName,adminoDto.Password,adminoDto.UserName,adminoDto.DoesItRemove,adminoDto.role, cancellationToken);
                TempData["success"] = "اطلاعات با موفقیت ثبت شد";
                return RedirectToAction("AdminShowDetailPanel");
            }
            else
            {
                TempData["error"] = "اطلاعات ثبت نشد دوباره سعی کنید";
            }
            #endregion
            return View();
        }
        #endregion
        #region ShowDetailPanel
        [HttpGet]
        public async Task<IActionResult> AdminShowDetailPanel(CancellationToken cancellationToken)
        {
            _logger.LogInformation("log in the AdminShowDetailPanel controller");
            var ListOfAmins= await _adminRepositoryAppService.GetAll(cancellationToken);
            return View(ListOfAmins);
        }
        #endregion
        #region Edit
        [HttpGet]
        public async Task<IActionResult> EditAdmin(long Id, CancellationToken cancellationToken)
        {
            var AdminDto = await _adminRepositoryAppService.GetById(Id, cancellationToken);
            if (AdminDto != null) {
                return View(AdminDto);
            }
            else
            {
                return RedirectToAction("EditAdmin");
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditAdmin(AdminDto objDto, CancellationToken cancellationToken)
        {
            #region Check string validation
            if (varstring.Verifystring(objDto.FullName) == true)
            {
                ModelState.AddModelError("نام و نام خانوادگی", " لطفانام و نام خانوادگی حروف وارد کنید");
            }
            if (varstring.Verifystring(objDto.UserName) == true)
            {
                ModelState.AddModelError("نام کاربری", " لطفا نام کاربری حروف وارد کنید");
            }
            #endregion
            #region check database attribute validation
            if (ModelState.IsValid && objDto != null)
            {
                await _adminRepositoryAppService.Edit(objDto.Id, objDto.FullName, objDto.Password, objDto.UserName, objDto.DoesItRemove, cancellationToken);
                TempData["success"] = "اطلاعات با موفقیت ثبت شد";
                return RedirectToAction("AdminShowDetailPanel");
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
        /*        [HttpPost]
                public async Task<IActionResult> DeleteAdmin(AdminDto objDto, CancellationToken cancellationToken)
                {
                    if (objDto != null)
                    {
                        await _adminRepositoryAppService.Remove(objDto.Id,cancellationToken);
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
