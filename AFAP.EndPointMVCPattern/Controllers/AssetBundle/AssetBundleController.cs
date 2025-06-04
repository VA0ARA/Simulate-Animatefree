using APAF.Domain.Core.Contracts.AppServices;
using Microsoft.AspNetCore.Mvc;
using APAF.Domain.Core.Dtos.AssetBundles;
using AFAP.EndPointMVCPattern.CustomValidators;
using Microsoft.AspNetCore.Authorization;
using AFAP.EndPointMVCPattern.Infrastructure;
using Microsoft.Extensions.Options;
namespace AFAP.EndPointMVCPattern.Controllers.AssetBundle
{
    [Authorize(Roles = "Owner ,Admin")]
    public class AssetBundleController : Controller
    {
        #region Property-Constructore
        private readonly IAssetBundleAppService _assetBundleRepositoryAppService;
        private readonly IMatchAppService _matchRepositoryAppService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private StringValidation varstring = new StringValidation();
        private readonly PathVariable _winVariable;
        public AssetBundleController(IAssetBundleAppService assetBundleRepositoryAppService, IMatchAppService matchRepositoryAppService, IWebHostEnvironment webHostEnvironment, IOptions<PathVariable> winVariable)
        {
            _assetBundleRepositoryAppService = assetBundleRepositoryAppService;
            _matchRepositoryAppService = matchRepositoryAppService;
            _webHostEnvironment = webHostEnvironment;
            _winVariable = winVariable.Value;
        }
        #endregion
        #region AssetbundleShowDetailPanel
        [HttpGet]
        public async Task<IActionResult> AssetbundleShowDetailPanel(CancellationToken cancellationToken)
        {
            var ListOfAsset = await _assetBundleRepositoryAppService.GetAll(cancellationToken);
            return View(ListOfAsset);
        }
        #endregion
        #region create
        [HttpGet]
        public IActionResult CreateAssetbundle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAssetbundle(AssetBundelDTos assetbandleDto, IFormFile file, IFormFile file2, CancellationToken cancellationToken)
        {
            #region Check string validation
            if (varstring.Verifystring(assetbandleDto.Name) == true)
            {
                ModelState.AddModelError("نام فایل حروف", " لطفا نام فایل را به حروف وارد کنید");
            }
            //if (varstring.Verifystring(assetbandleDto.MatchObj.Name) == true)
            //{
                //ModelState.AddModelError("نام مسابقه حروف", " لطفا نام مسایقه را به  حروف وارد کنید");
            //}
            if (file == null && file2 == null)
            {
                ModelState.AddModelError("وضعیت آپلود فایلها", " لطفا فایل ها را اپلود کنید");
            }
            #region check file format
            //
            if (file != null && file2 != null)
            {
                if (Path.GetExtension(file.FileName) != ".png")
                {
                    ModelState.AddModelError("فرمت  عکس", "   فرمت  عکس را درست وارد کنید (png.)");
                }
                if (Path.GetExtension(file2.FileName) != ".anim")
                {
                    ModelState.AddModelError("فرمت فایل", "  فرمت فایل  را درست وارد کنید (anim.)");
                }
            }
            #endregion
            #endregion
            if (ModelState.IsValid){
            if (file != null && file2!=null)

            {
                string fileName = Guid.NewGuid().ToString() + file.FileName;
                string fileName2 = Guid.NewGuid().ToString() + file2.FileName;
                var nestedFolderPath = _winVariable.Main1Path + _winVariable.Main2Path + _winVariable.AssetbannerPath;
                var nestedFolderPath2 = _winVariable.Main1Path + _winVariable.Main2Path + _winVariable.AssetFilePath;
                #region Banner
                    string bannerPath = _winVariable.Main1Path + _winVariable.Main2Path + _winVariable.AssetbannerPath;
                if (file != null)
                {
                    // findout we are in edit page delete old file
                    if (!string.IsNullOrEmpty(assetbandleDto.Banner))
                    {
                       var Oldbanner = _winVariable.Main1Path + assetbandleDto.Banner;
                            if (System.IO.File.Exists(Oldbanner))
                        {
                            System.IO.File.Delete(Oldbanner);
                        }
                    }
                    //create new file
                    using (var fileStream = new FileStream(bannerPath+fileName, FileMode.Create))//Path.Combine(bannerPath, fileName)
                        {
                        file.CopyTo(fileStream);
                    }
                    assetbandleDto.Banner = _winVariable.Main2Path+_winVariable.AssetbannerPath + fileName;
                }
                #endregion
                #region AnimFile
                if (file2 != null)
                {

                    string AssetPath = _winVariable.Main1Path + _winVariable.Main2Path + _winVariable.AssetFilePath;

                        if (!string.IsNullOrEmpty(assetbandleDto.filepath))
                        {
                         var Oldbanner = _winVariable.Main1Path + assetbandleDto.filepath;
                            if (System.IO.File.Exists(Oldbanner))
                            {
                            System.IO.File.Delete(Oldbanner);
                            }
                        }
                    using (var fileStream = new FileStream(AssetPath+fileName2, FileMode.Create))//Path.Combine(AssetPath, fileName2)
                        {
                        file2.CopyTo(fileStream);
                    }
                    assetbandleDto.filepath = _winVariable.Main2Path + _winVariable.AssetFilePath + fileName2;
                #endregion
                }
                #region CRUD

                await _assetBundleRepositoryAppService.Create(assetbandleDto.Name, assetbandleDto.filepath, assetbandleDto.UploadedOn, assetbandleDto.Price, assetbandleDto.DoesItRemove, assetbandleDto.Type, assetbandleDto.Banner, "coffeebazarxfhd", assetbandleDto.doesitblongethematch, cancellationToken);
                var AssetBundellast = (await _assetBundleRepositoryAppService.GetAll(cancellationToken)).LastOrDefault();
                //await _matchRepositoryAppService.Create(assetbandleDto.MatchObj.Name, assetbandleDto.MatchObj.Score, assetbandleDto.MatchObj.Type, AssetBundellast.Id, assetbandleDto.MatchObj.DoesItRemove, cancellationToken);
                TempData["success"] = "اطلاعات با موفقیت ثبت شد";
                return RedirectToAction("AssetbundleShowDetailPanel");
                #endregion
            }
            }
            else
            {
                TempData["error"] = "اطلاعات ثبت نشد دوباره سعی کنید";
            }
            return View();
        }
        #endregion
        #region Edit
        [HttpGet]
        public async Task<IActionResult>  EditAssetbundle(long Id,CancellationToken cancellationToken)
        {
                var assetDto = await _assetBundleRepositoryAppService.GetById(Id, cancellationToken);
                //var matchDto =(await _matchRepositoryAppService.GetAll(cancellationToken)).Where(x=>x.AssetBundlesId==assetDto.Id).FirstOrDefault();
               // assetDto.MatchObj = matchDto;
                  return View(assetDto);
        }
        [HttpPost]
        public async Task<IActionResult> EditAssetbundle(AssetBundelDTos assetbandleDto, IFormFile? file, IFormFile? file2, CancellationToken cancellationToken)
        {
            #region Check string validation
            if (varstring.Verifystring(assetbandleDto.Name) == true)
            {
                ModelState.AddModelError("نام فایل", " لطفا نام فایل را به حروف وارد کنید");
            }
           // if (varstring.Verifystring(assetbandleDto.MatchObj.Name) == true)
            //{
               // ModelState.AddModelError("نام مسابقه", " لطفا نام مسابقه را به  حروف وارد کنید");
           // }
            #region check file format
            //
            if (file != null && file2 != null)
            {
                //consider last files
                if (Path.GetExtension(file.FileName) != ".png")
                {
                    ModelState.AddModelError("فرمت عکس", "   فرمت  عکس را درست وارد کنید (png.)");
                }
                if (Path.GetExtension(file2.FileName) != ".anim")
                {
                    ModelState.AddModelError("فرمت فایل", "  فرمت فایل  را درست وارد کنید (anim.)");
                }
            }
            else if (file == null && file2 != null)
            {
                if (Path.GetExtension(file2.FileName) != ".anim")
                {
                    ModelState.AddModelError("فرمت فایل", "  فرمت فایل  را درست وارد کنید (anim.)");
                }
            }
            else if (file != null && file2 == null)
            {
                if (Path.GetExtension(file.FileName) != ".png")
                {
                    ModelState.AddModelError("فرمت عکس", "   فرمت  عکس را درست وارد کنید (png.)");
                }
            }
            //else if (file == null && file2 == null)
           // {
                    //ModelState.AddModelError("هیچ", "  تغییری صورت نگرفت ");
            //}
            #endregion
            #endregion
            if (ModelState.IsValid)
            {
                #region Banner
                var nestedFolderPath = _winVariable.Main1Path + _winVariable.Main2Path + _winVariable.AssetbannerPath;
                var banner =  _winVariable.Main2Path + _winVariable.AssetbannerPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                // findout we are in edit page delete old file
                if (!string.IsNullOrEmpty(assetbandleDto.Banner))
                {
                        //var Oldbanner =Path.Combine(_winVariable.Main1Path , assetbandleDto.Banner.TrimStart('\\'));
                        var Oldbanner = _winVariable.Main1Path + assetbandleDto.Banner;
                        if (System.IO.File.Exists(Oldbanner))
                    {
                        System.IO.File.Delete(Oldbanner);
                    }
                }
                //create new file
                using (var fileStream = new FileStream(nestedFolderPath+fileName, FileMode.Create))//Path.Combine(nestedFolderPath, fileName)
                    {
                        file.CopyTo(fileStream);
                    }
                assetbandleDto.Banner = banner + fileName;
                }else{
                    assetbandleDto.Banner = assetbandleDto.Banner;
                }
                #endregion
                #region AnimFile
                var nestedFolderPath2 = _winVariable.Main1Path +_winVariable.Main2Path+ _winVariable.AssetFilePath;
                var filetemp = _winVariable.Main2Path + _winVariable.AssetFilePath;
                if (file2 != null)
                {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file2.FileName);
                if (!string.IsNullOrEmpty(assetbandleDto.filepath))
                {
                        //windows
                        //var Oldbanner = Path.Combine(_winVariable.Main1Path, assetbandleDto.filepath.TrimStart('\\'));
                        var Oldbanner = _winVariable.Main1Path+assetbandleDto.filepath;
                        if (System.IO.File.Exists(Oldbanner))
                    {
                        System.IO.File.Delete(Oldbanner);
                    }
                }
                using (var fileStream = new FileStream(nestedFolderPath2+fileName, FileMode.Create))//Path.Combine(nestedFolderPath2, fileName)
                    {
                    file2.CopyTo(fileStream);
                }
                assetbandleDto.filepath = filetemp + fileName;
            }else{
                assetbandleDto.filepath=assetbandleDto.filepath;
            }

            #endregion
            #region check database attribute validation

                await _assetBundleRepositoryAppService.Edit(assetbandleDto.Id, assetbandleDto.Name, assetbandleDto.filepath, assetbandleDto.UploadedOn, assetbandleDto.Price, assetbandleDto.DoesItRemove, assetbandleDto.Type, assetbandleDto.Banner, "coffeebazarxfhd", assetbandleDto.doesitblongethematch, cancellationToken);
                //await _matchRepositoryAppService.Edit(assetbandleDto.MatchObj.Id, assetbandleDto.MatchObj.Name, assetbandleDto.MatchObj.Score, assetbandleDto.MatchObj.Type, assetbandleDto.Id, assetbandleDto.MatchObj.DoesItRemove, cancellationToken);
                TempData["success"] = "اطلاعات با موفقیت ثبت شد";
                return RedirectToAction("AssetbundleShowDetailPanel");
            }
            else
            {
                TempData["error"] = "اطلاعات ثبت نشد دوباره سعی کنید";
            }
            return RedirectToAction("EditAssetbundle");
            #endregion
        }
        #endregion
        #region Delete
                [HttpPost]
                public async Task<IActionResult> DeleteAssetBundel(long Id, CancellationToken cancellationToken)
                {
                var tempAsset= await _assetBundleRepositoryAppService.GetById(Id, cancellationToken);
                if (tempAsset != null)
                    {
                #region deletefrom physical-Folders 
                //1.delet banner
                if (tempAsset.Banner!= null)
                {
                // findout we are in edit page delete old file
                if (!string.IsNullOrEmpty(tempAsset.Banner))
                {
                        var Oldbanner = _winVariable.Main1Path + tempAsset.Banner;
                    if (System.IO.File.Exists(Oldbanner))
                    {
                        System.IO.File.Delete(Oldbanner);
                    }
                }
                }
                //2.delete file
                if (tempAsset.filepath!= null)
                {
                if (!string.IsNullOrEmpty(tempAsset.filepath))
                {
                        var Oldbanner = _winVariable.Main1Path+tempAsset.filepath;
                        if (System.IO.File.Exists(Oldbanner))
                    {
                        System.IO.File.Delete(Oldbanner);
                    }
                }
                }
                #endregion
                //3.delete from databse
                await _assetBundleRepositoryAppService.Remove(tempAsset.Id,cancellationToken);
                return RedirectToAction("AssetbundleShowDetailPanel");
                }
                return RedirectToAction("AssetbundleShowDetailPanel");
               
            }
            #endregion
}
}
