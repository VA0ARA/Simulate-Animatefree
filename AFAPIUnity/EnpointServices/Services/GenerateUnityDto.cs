using AFAPIUnity.EnpointServices.Contract;
using APAF.Domain.Core.Contracts.AppServices;
using APAF.Domain.Core.Dtos.Animators;
using APAF.Domain.Core.Dtos.AssetBundles;
using APAF.Domain.Core.Dtos.Avatars;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
namespace AFAPIUnity.EnpointServices.Services
{
    public class GenerateUnityDto : ControllerBase, IGenerateUnityDto 
    {
        #region property-Constructor
        private readonly ISetAssetAppService _setAssetAppService;
        private readonly IAnimatorAppService _animatorRepositoryAppService;
        private readonly PathVariable _winVariable;
        private readonly string _externalFolderPath = @"D:\0000000000000000000000000000000";
        private readonly IMyUrlService _myUrlService;
        private readonly IAssetBundelAnimatorAppService _assetBundelAnimatorAppService;
        public GenerateUnityDto(ISetAssetAppService setAssetAppService, IAnimatorAppService animatorRepositoryAppService,IMyUrlService myUrlService, IAssetBundelAnimatorAppService assetBundelAnimatorAppService, IOptions<PathVariable> winVariable)
        {
            _setAssetAppService = setAssetAppService;
            _animatorRepositoryAppService = animatorRepositoryAppService;
            _myUrlService = myUrlService;
            _assetBundelAnimatorAppService = assetBundelAnimatorAppService;
            _winVariable = winVariable.Value;
        }
        #endregion
        #region Implementation
        //RemoveDueToNDA
            return (animator);
        }
        //generate assetbunle by links of filepath 
        public List<AssetBundelDTos> GenerateAssetbunderl(List<AssetBundelDTos>Assets)
        {
            #region AssetBunel File                             
            var wwwRootPath = _winVariable.Main1Path + _winVariable.Main2Path + _winVariable.AssetFilePath;
            var files = Directory.GetFiles(wwwRootPath);
            #endregion
            #region Banner
            #endregion
            return (Assets);
        }
        //generate avatar by links of filepath 
        public List<AvatarDtos> GenerateAvatar(List<AvatarDtos> Avatars)
        {
     
            return(Avatars);
        }
        //generate avatar by link of filepath  with Id 
        public async Task< AvatarDtos> GenerateAvatarById(long id, CancellationToken cancellationToken)
        {
            var Avatars = await _setAssetAppService.SetAvatarById(id,cancellationToken);
    
            return (Avatars);
        }
        #endregion
    }
}
