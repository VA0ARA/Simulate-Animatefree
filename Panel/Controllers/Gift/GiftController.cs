using APAF.Domain.Core.Contracts.AppServices;
using APAF.Domain.Core.Contracts.Services;
using APAF.Domain.Core.Dtos.Gifts;
using Microsoft.AspNetCore.Mvc;
using Panel.CustomValidation;
using Panel.PanelServices;

namespace Panel.Controllers.Gift
{
    public class GiftController : Controller
    {
        #region Property-Constructore
        public const string  MainPath="/Exter*****atic******iles";
        public const string GiftPath="Gifts";
        public CreateFolderName objCreateFolderName;
        private readonly IGiftService _GiftService;
        private readonly IFileManagerService _filemanagerService;
        private StringValidation varstring;
         public GiftController(IGiftService giftService,IFileManagerService filemanager)
         {
             _GiftService = giftService;
             _filemanagerService = filemanager;
             objCreateFolderName=new CreateFolderName();
             varstring = new StringValidation();
         }
        #endregion
        #region ShowListOfAvatars
        [HttpGet]
        public async Task<IActionResult> ListOfGifts(CancellationToken cancellationToken)
        {
            var ListOfAvatar = await _GiftService.GetAll(1, 25,cancellationToken);
            //var ListOfAvatar = await _avatarRepositoryService.GetAll(int pageNumber, int pageSize,cancellationToken);
            return View(ListOfAvatar);
            
        }
        #endregion
        #region Create
        [HttpGet]
        public IActionResult CreateGift( CancellationToken cancellationToken)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAvatar(GiftDto giftDto, IFormFile file, CancellationToken cancellationToken)
        {
            #region Check string validation
            if (varstring.Verifystring(giftDto.Name) == true)
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
            #region When ModelSate is True
            if (ModelState.IsValid)
            {   
            // I remove Space and other stuff From name of folder to create right directory
            objCreateFolderName.Name=giftDto.Name;
            string TempFolderName=objCreateFolderName.GenerateName();
            //I m gonna predicted the path of Avatar file
             string FolderOfGift=MainPath+GiftPath+TempFolderName;
            if (file != null)
                {
                //does FolderOfAvatar direcotry exist?
                bool DoesItExsitDirectoy=await _filemanagerService.DirectoryExistsAsync(FolderOfGift);
                if(DoesItExsitDirectoy ==false)
                {
                    //I create  a directory!
                    await _filemanagerService.CreateDirectoryAsync(FolderOfGift);
                    // i m gonna  make the exact directory!
                    string CompeleteDirectionFile = Path.Combine(FolderOfGift, file.FileName); 
                    //I  m gonna  copy a File in the FolderOfAvatar 
                    await _filemanagerService.SaveFileAsync(CompeleteDirectionFile,file,cancellationToken);
                    //I insert attribute of Filepath in my Avatar Table
                    giftDto.PathOfFile=CompeleteDirectionFile;
                }
                else{
                    // so!!! we have folder for some Raeson we do not care about it 
                    // i m gonna  make the exact directory!
                    string CompeleteDirectionFile = Path.Combine(FolderOfGift, file.FileName); 
                    //I  m gonna  copy a File in the FolderOfAvatar 
                    await _filemanagerService.SaveFileAsync(CompeleteDirectionFile,file,cancellationToken);
                    //I insert attribute of Filepath in my Avatar Table
                    giftDto.PathOfFile=CompeleteDirectionFile;
                
                    }
                // So Now! I m gonna insert All Data to Database
                await _GiftService.Create(giftDto,cancellationToken);
                TempData["success"] = "اطلاعات با موفقیت ثبت شد";
                return RedirectToAction("ListOfAvatars");
                }
            }
            #endregion
            if (giftDto.PathOfFile == null)
            {
                ModelState.AddModelError(" فایل", " لطفا  فایل را آپلود کنید");
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
        public async Task<IActionResult> EditAvatar(int id, CancellationToken cancellationToken)
        {
                var GitfDto = await _GiftService.GetById(id, cancellationToken);
                return View(GitfDto);
            }
        [HttpPost]
        public async Task<IActionResult> EditAvatar(GiftDto giftDto, IFormFile? file, CancellationToken cancellationToken)
        {
        #region Check string validation
            if (varstring.Verifystring(giftDto.Name) == true)
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
            #endregion
            #region File
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    // I remove Space and other stuff From name of folder to create right directory
                    objCreateFolderName.Name=giftDto.Name;
                    string TempFolderName=objCreateFolderName.GenerateName();
                    //I m gonna predicted the path of Avatar file
                    string NewFolderOfAvatar=MainPath+GiftPath+TempFolderName;
                       //I m gonna delete exsiting old file 
                      await _filemanagerService.DeleteFileAsync(giftDto.PathOfFile);
                      //then create NewFolder And delete old one
                       string oldFolder= _filemanagerService.GetLastSegment(giftDto.PathOfFile);
                       string basepath =MainPath+GiftPath;
                       string newpath=Path.Combine(NewFolderOfAvatar, file.FileName);
                      await _filemanagerService.ManageFolderAsynic(basepath,oldFolder,TempFolderName);
                      //then copy new file***************
                      await _filemanagerService.SaveFileAsync(newpath,file,cancellationToken);
                      //send data to data base
                      await _GiftService.Edit(giftDto, cancellationToken);
                       TempData["success"] = "اطلاعات با موفقیت ثبت شد";
                    return RedirectToAction("ListOfAvatars");
                }
            
                }else{
                    giftDto.PathOfFile = giftDto.PathOfFile;
                    TempData["error"] = "اطلاعات ثبت نشد دوباره سعی کنید";
                }
             return View();
            }
       #endregion
          
        #endregion
        #region Delete
            //     [HttpPost]
            //     public async Task<IActionResult> DeleteAvatar(long Id, CancellationToken cancellationToken)
            //     {
            //     var tempavatar= await _avatarRepositoryAppService.GetById(Id, cancellationToken);
            //     if (tempavatar != null)
            //         {
            //     #region deletefrom physical-Folders 
            //     //1.delet banner
            //     if (tempavatar.FilePath!= null)
            //     {
            //     // findout we are in edit page delete old file
            //     if (!string.IsNullOrEmpty(tempavatar.FilePath))
            //     {
            //             var Oldbavatar = _winVariable.Main1Path + tempavatar.FilePath;
            //         if (System.IO.File.Exists(Oldbavatar))
            //         {
            //             System.IO.File.Delete(Oldbavatar);
            //         }
            //     }
            //     }
            //     #endregion
            //     //3.delete from databse
            //     await _avatarRepositoryAppService.Remove(tempavatar.Id,cancellationToken);
            //     return RedirectToAction("AvatarShowDetailPanel");
            //     }
            //     return RedirectToAction("AvatarShowDetailPanel");
            // }
            #endregion


    }
}
