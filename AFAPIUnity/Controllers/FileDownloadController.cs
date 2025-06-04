using AFAPIUnity.EnpointServices.Contract;
using AFAPIUnity.EnpointServices.Services;
using APAF.Domain.Core.Contracts.AppServices;
using APAF.Domain.Core.Dtos.Animators;
using APAF.Domain.Core.Dtos.AssetBundles;
using APAF.Domain.Core.Dtos.Avatars;
using APAF.Domain.Core.Entities.AssetBundel_Animator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using APAF.Domain.Core.Contracts.Repository;
using APAF.Domain.Core.Contracts.Services;
using Microsoft.AspNetCore.Razor.Language;
namespace AFAPIUnity.Controllers
{
    [Route("[controller]")]
    //[Authorize]
    //[Authorize(Policy = "AnimatorOnly")]
    public class FileDownloadController : ControllerBase
    {
        #region property-Constructor
        private readonly ISetAssetAppService _setAssetAppService;
        private readonly IAssetBundelAnimatorAppService _assetBundelAnimatoreAppService;
         private readonly IAnimatorAppService _animatorRepositoryAppService;
        private readonly IGenerateUnityDto _generateUnityDto;
        private readonly PathVariable _winVariable;
        public FileDownloadController(ISetAssetAppService setAssetAppService, IAssetBundelAnimatorAppService assetBundelAnimatoreAppService, IGenerateUnityDto generateUnityDto, IOptions<PathVariable> winVariable,IAnimatorAppService animatorRepositoryAppService)
        {
            _setAssetAppService = setAssetAppService;
            _assetBundelAnimatoreAppService = assetBundelAnimatoreAppService;
            _generateUnityDto = generateUnityDto;
            _winVariable = winVariable.Value;
            _animatorRepositoryAppService=animatorRepositoryAppService;
        }
        #endregion
        #region SendAnimatorProfileDetail
        [HttpPost("SendAnimatorProfileDetail")]
        [Authorize(Policy = "AnimatorOnly")]
        public async Task< IActionResult> SendAnimatorProfileDetail([FromForm]long Id, CancellationToken cancellationToken)
        {
            ///RemoveDueToNDA
            if (temp.Id != 0)
            {
                //add link
                return Ok(new {Data=temp});
            }
            else
            {
                return NotFound();
            }
        }
        #endregion
        #region GenerateListAssetBundel
        //[Authorize(Policy = "GuestOnly")]
        //[Authorize(Policy = "AnimatorOnly")]
        [HttpGet("GenerateListOfAllAssetBundel")]
        public async Task<IActionResult> GenerateListOfAllAssetBundel(CancellationToken cancellationToken)
        {
            //****************RemoveDueToNDA*************
            return Ok(new{Data=ListOfAllAssets});
        }
        //[Authorize(Policy = "GuestOnly")]
        [HttpGet("GenerateListOfAllFreeAssetBundel")]
        public async Task<IActionResult> GenerateListOfAllFreeAssetBundel(CancellationToken cancellationToken)
        {
            //****************RemoveDueToNDA*************
            return Ok(new{Data=ListOfAllAssets});
        }
        [HttpGet("downloadAssetBundelFileService")]
        public IActionResult downloadAssetBundelFileService(string fileName)
        {
            //****************RemoveDueToNDA*************

            return File(fileBytes, fileType);
        }
        [HttpGet("downloadAssetBundelBannerService")]
        public IActionResult downloadAssetBundelBannerService(string fileName)
        {
            //****************RemoveDueToNDA*************
            return File(fileBytes, fileType, fileName);
        }
        #endregion
        #region GenerateListOfAllAvatar
        [HttpGet("GenerateListOfAllAvatar")]
        public async Task<IActionResult> GenerateListOfAllAvatar(CancellationToken cancellationToken)
        {
            //****************RemoveDueToNDA*************
            return Ok(new {Data=ListOfAllAVatar});
        }
        [HttpGet("downloadAvatarService")]
        public IActionResult downloadAvatarService(string fileName)
        {
            //****************RemoveDueToNDA*************
            return File(fileBytes, fileType, fileName);
        }
        #endregion
        //.net get form unity Asset that was bought by animator.
        #region GetShopAsssetOfAnimator
        [HttpPost("GetShopAsssetOfAnimator")]
        [Authorize(Policy = "AnimatorOnly")]
        public async Task<IActionResult> GetShopAsssetOfAnimator([FromForm]RequestShopAsset requestasset, CancellationToken cancellationToken)
        {
            //****************RemoveDueToNDA*************
        }
        }
        #endregion
    }
}

