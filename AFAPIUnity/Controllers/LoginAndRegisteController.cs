using Microsoft.AspNetCore.Mvc;
using AFAPIUnity.TokenService;
using APAF.Domain.Core.Contracts.AppServices;
using AFAPIUnity.Dtos;
using AFAPIUnity.CustomValidators;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using APAF.Domain.Core.Dtos.AssetBundles;
using APAF.Domain.Core.Entities.AssetBundles;
using APAF.Domain.Core.Entities.AssetBundel_Animator;
using APAF.Domain.Core.Entities.Avatars;
using APAF.Domain.Core.Enums;
using Kavenegar;
using Kavenegar.Exceptions;
using APAF.Domain.Core.Dtos.Animators;
using APAF.Domain.Core.Contract.AppServices;
namespace AFAPIUnity.Controllers
{
    [Route("[controller]")]
    public class LoginAndRegisterController :  ControllerBase
    {
        #region property-Constructor
        private readonly IGenerateToken _generateToken;
        private readonly IAPKAppService _apk;
        private readonly IAnimatorAppService _animatorRepositoryAppService;
        private readonly IAssetBundleAppService _assetBundleAppService;
        private readonly ISetAssetAppService _setAssetAppService;
        private readonly ILogger<LoginAndRegisterController> _logger;
        private StringAndNumberValidation varstring = new StringAndNumberValidation();
        public LoginAndRegisterController(IGenerateToken generateToken,IAnimatorAppService animatorRepositoryAppService, ISetAssetAppService setAssetAppService,ILogger<LoginAndRegisterController> logger,IAssetBundleAppService assetBundleAppService,IAPKAppService apk)
        {
            _generateToken = generateToken;
            _animatorRepositoryAppService = animatorRepositoryAppService;
            _setAssetAppService = setAssetAppService;
            _logger = logger;
            _assetBundleAppService = assetBundleAppService;
            _apk=apk;
        }
        #endregion
        #region Register
         [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> RequestOfRegisterAndSendDefualtData([FromForm] RegisterSendDefulatDataDto animator, CancellationToken cancellationToken)
        {
            #region Check string-number validation
            //RemoveDueToNDA
            #endregion
            #region Check phone-and create Animator
            //RemoveDueToNDA
            #endregion
        }
        #endregion
        #region Login
        [Authorize(Policy = "GuestOnly")]
        [HttpPost("Login")]
        public async Task< IActionResult> Login([FromForm] PhoneNumberRequest request,CancellationToken cancellationToken)
        {
            _logger.LogInformation("log int he index controller");
            //1.does it exsit
            var AnimatorDto = await _animatorRepositoryAppService.GetByPhone(request.PhoneNumber, cancellationToken);
            //2.send token
            if (ModelState.IsValid)
            {
                if (AnimatorDto.FullName != null)
                {
                    var expireAt = DateTime.UtcNow.AddDays(30);
                    //var claims = _generateToken.CreateClaims();
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,AnimatorDto.FullName ),
                        new Claim(ClaimTypes.Name,AnimatorDto.PhoneNumber)//,
                        //new Claim(ClaimTypes.Role, "guestAnimNitech123!@#") 
                    };
                    //var animatorId=AnimatorDto.Id;
                    var token = _generateToken.CtrateToken(claims, expireAt);
                    return Ok(new
                    {
                        Token= token, 
                        expires_at = expireAt,
                        Id=AnimatorDto.Id
                    });
                }
                else
                {
                    return NotFound("This person does not exsit");
                }
            }
            else
            {
                ModelState.AddModelError("Unauthorized", "You are not authorized to access the endpoint.");
                return Unauthorized(ModelState);

            }
        }
        #endregion
        #region generate-guest-token
         [AllowAnonymous]
    [HttpPost("generate-guest-token")]
    public ActionResult<string> GenerateGuestToken([FromForm] APIKeyToken key)
    {
        if(key.KeyGuest=="******************************"){

        var Token = _generateToken.CreateGusetToken(DateTime.UtcNow.AddHours(7));
            //return Ok(token);
            return Ok(new { token = Token });
        }
        else
        {
            return BadRequest("god deam! key is Wrong man");
        }
    }
    #endregion
        #region  SMS 
         [AllowAnonymous]
        [HttpPost("SendSms")]
       public async Task<IActionResult> SendSms([FromForm] SmsRequest request)
{
            Random random = new Random();
        int randomNumber = random.Next(100000, 999999);
    if (string.IsNullOrEmpty(request.Receptor))// || string.IsNullOrEmpty(request.Message))
    {
        return BadRequest("Receptor and message are required.");
    }

    try
    {
        var apiKey = "******************************************************"; // Use your actual API key
        var link = $"https://api.kavenegar.com/v1/{apiKey}/verify/lookup.json?receptor={request.Receptor}&token={randomNumber}&template=AnimateFree";

        using (var httpClient = new HttpClient())
        {
            // Send the request
            var response = await httpClient.GetAsync(link);
            if (response.IsSuccessStatusCode)
            {
                // Handle the successful response
                var responseBody = await response.Content.ReadAsStringAsync();
                return Ok(responseBody); // Return the response from Kavenegar
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Error calling Kavenegar API.");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Internal Error: {ex.Message}");
        return StatusCode(500, $"Internal Server Error: {ex.Message}");
    }
}
        #endregion
        #region EditProfile
        [Authorize(Policy = "AnimatorOnly")]
        [HttpPost("GetEditProfile")]
         public async Task<IActionResult> GetEditProfile([FromForm] long Id, CancellationToken cancellationToken){

            //RemoveDueToNDA
             return BadRequest("not null -not found ");
         }
         [Authorize(Policy = "AnimatorOnly")]
         [HttpPost("PostEditeprofile")]
         public async Task<IActionResult> PostEditProfile([FromForm] AnimatorDtoEdit animator, CancellationToken cancellationToken){
            //RemoveDueToNDA
          return BadRequest("not null -not found ");
         }
        #endregion
        #region VersionOfAPK
         [AllowAnonymous]
        [HttpGet("GetLastVersion")]
        public async Task<IActionResult>GetLastVersion(CancellationToken cancellationToken){
            var APK=(await _apk.GetAll(cancellationToken)).LastOrDefault();
            if(APK!=null){
            return Ok(APK);
            }
            return NotFound();
        }
        #endregion 
    }
}
