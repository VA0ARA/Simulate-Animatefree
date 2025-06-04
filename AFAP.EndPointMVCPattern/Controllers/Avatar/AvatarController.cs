using AFAP.EndPointMVCPattern.CustomValidators;
using AFAP.EndPointMVCPattern.Infrastructure;
using APAF.Domain.Core.Contracts.AppServices;
using APAF.Domain.Core.Dtos.Avatars;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
namespace AFAP.EndPointMVCPattern.Controllers.Avatar
{
    [Authorize(Roles = "Owner ,Admin")]
    public class AvatarController : Controller
    {
        #region Property-Constructore
        private readonly IAvatarAppService _avatarRepositoryAppService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private StringValidation varstring = new StringValidation();
        private readonly PathVariable _winVariable;
        public AvatarController(IAvatarAppService avatarRepositoryAppService, IWebHostEnvironment webHostEnvironment, IOptions<PathVariable> winVariable)
        {
            _avatarRepositoryAppService = avatarRepositoryAppService;
            _webHostEnvironment = webHostEnvironment;
            _winVariable = winVariable.Value;
        }
        #endregion
        #region AvatarShowDetailPanel
        [HttpGet]
        public async Task<IActionResult> AvatarShowDetailPanel(CancellationToken cancellationToken)
        {
            var ListOfAvatar = await _avatarRepositoryAppService.GetAll(cancellationToken);
            return View(ListOfAvatar);
        }
        #endregion
        #region Create
        [HttpGet]
        public async Task<IActionResult> CreateAvatar( CancellationToken cancellationToken)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAvatar(AvatarDtos AvatarDto, IFormFile file, CancellationToken cancellationToken)
        {
            #region Check string validation
            if (varstring.Verifystring(AvatarDto.Name) == true)
            {
                ModelState.AddModelError("نام فایل", " لطفا نام فایل را به حروف وارد کنید");
            }
            if (file == null)
            {
                ModelState.AddModelError("فایل ", " لطفا فایل ها را اپلود کنید");
            }
            if (file != null)
            {
                if (Path.GetExtension(file.FileName) != ".png")
                {
                    ModelState.AddModelError("نام مسابقه", " فرمت فایل (png.)");
                }
            }
            #endregion
            #region File
            if (ModelState.IsValid)
            {
                var nestedFolderPath = _winVariable.Main1Path + _winVariable.Main2Path + _winVariable.AvatarPath;
                if (file != null)
                {
                string fileName = Guid.NewGuid().ToString() + file.FileName;
                string avatarPath = _winVariable.Main1Path + _winVariable.Main2Path + _winVariable.AvatarPath;
                    if (!string.IsNullOrEmpty(AvatarDto.FilePath))
                    {
                        var Oldbanner = avatarPath + AvatarDto.FilePath;
                        if (System.IO.File.Exists(Oldbanner))
                        {
                            System.IO.File.Delete(Oldbanner);
                        }
                    }
                    //create new file
                    using (var fileStream = new FileStream(avatarPath+fileName, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    AvatarDto.FilePath = _winVariable.Main2Path + _winVariable.AvatarPath + fileName;
                }
            if (AvatarDto.FilePath == null)
            {
                ModelState.AddModelError(" فایل", " لطفا  فایل را آپلود کنید");
            }

            #region CRUD

                    await _avatarRepositoryAppService.Create(AvatarDto.Name, AvatarDto.FilePath, AvatarDto.DoesItRemove, AvatarDto.UploadedOn, cancellationToken);
                    TempData["success"] = "اطلاعات با موفقیت ثبت شد";
                    return RedirectToAction("AvatarShowDetailPanel");
                #endregion
            }
            else
            {
                TempData["error"] = "اطلاعات ثبت نشد دوباره سعی کنید";
            }
            #endregion
            return View();
        }
        #endregion
        #region Edit
        [HttpGet]
        public async Task<IActionResult> EditAvatar(long id, CancellationToken cancellationToken)
        {
                var avatarDto = await _avatarRepositoryAppService.GetById(id, cancellationToken);
                return View(avatarDto);
            }
        [HttpPost]
        public async Task<IActionResult> EditAvatar(AvatarDtos AvatarDto, IFormFile? file, CancellationToken cancellationToken)
        {
        #region Check string validation
            if (varstring.Verifystring(AvatarDto.Name) == true)
            {
                ModelState.AddModelError("نام فایل", " لطفا نام فایل را به حروف وارد کنید");
            }
            if (file != null)
            {
                if (Path.GetExtension(file.FileName) != ".png")
                {
                    ModelState.AddModelError("نام مسابقه", " فرمت فایل (png.)");
                }
            }
            //else if (file == null)
            //{
               // ModelState.AddModelError("هیچ", "  تغییری صورت نگرفت ");
            //}
            #endregion
            #region File
            if (ModelState.IsValid)
            {
                var nestedFolderPath = _winVariable.Main1Path + _winVariable.Main2Path + _winVariable.AvatarPath;
                var avatarPath = _winVariable.Main2Path + _winVariable.AvatarPath;

                if (file != null)
                {
                 string fileName = Guid.NewGuid().ToString() + file.FileName;
                    if (!string.IsNullOrEmpty(AvatarDto.FilePath))
                    {
                        var Oldbanner = _winVariable.Main1Path+ AvatarDto.FilePath;
                        if (System.IO.File.Exists(Oldbanner))
                        {
                            System.IO.File.Delete(Oldbanner);
                        }
                    }
                //create new file
                using (var fileStream = new FileStream(nestedFolderPath+fileName, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                AvatarDto.FilePath = _winVariable.Main2Path + _winVariable.AvatarPath + fileName;
                }else{
                    AvatarDto.FilePath = AvatarDto.FilePath;
                }
            //if (AvatarDto.FilePath == null)
            //{
                //ModelState.AddModelError(" فایل", " لطفا  فایل را آپلود کنید");
            //}
            #region CRUD
            await _avatarRepositoryAppService.Edit(AvatarDto.Id, AvatarDto.Name, AvatarDto.FilePath, AvatarDto.DoesItRemove, AvatarDto.UploadedOn, cancellationToken);
               TempData["success"] = "اطلاعات با موفقیت ثبت شد";
                return RedirectToAction("AvatarShowDetailPanel");
            #endregion
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
                public async Task<IActionResult> DeleteAvatar(long Id, CancellationToken cancellationToken)
                {
                var tempavatar= await _avatarRepositoryAppService.GetById(Id, cancellationToken);
                if (tempavatar != null)
                    {
                #region deletefrom physical-Folders 
                //1.delet banner
                if (tempavatar.FilePath!= null)
                {
                // findout we are in edit page delete old file
                if (!string.IsNullOrEmpty(tempavatar.FilePath))
                {
                        var Oldbavatar = _winVariable.Main1Path + tempavatar.FilePath;
                    if (System.IO.File.Exists(Oldbavatar))
                    {
                        System.IO.File.Delete(Oldbavatar);
                    }
                }
                }
                #endregion
                //3.delete from databse
                await _avatarRepositoryAppService.Remove(tempavatar.Id,cancellationToken);
                return RedirectToAction("AvatarShowDetailPanel");
                }
                return RedirectToAction("AvatarShowDetailPanel");
            }
            #endregion


            
    }
}
