using APAF.Domain.Core.Dtos.Account;
using APAF.Domain.Core.Entities.IdentityEntities;
using APAF.Domain.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AFAP.EndPointMVCPattern.Controllers.Register
{
    [Authorize(Roles = "Owner")]
    public class RegisterController : Controller
    {
        #region Prop
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RegisterController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        #endregion
        #region Register
        [HttpGet]
        public IActionResult RegisterView()
        {
            return View();
        }
       // [Authorize(Roles = "Owner")]
        [HttpPost]
        public async Task<IActionResult> RegisterView(RegisterDTO registerDTO)
        {
            //Check for validation errors
            if (ModelState.IsValid == false)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                return View(registerDTO);
            }
            ApplicationUser user = new ApplicationUser() { Email = registerDTO.Email, PhoneNumber = registerDTO.Phone, UserName = registerDTO.Email, PersonName = registerDTO.PersonName };
            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (result.Succeeded)
            {
                //Check status of radio button
                if (registerDTO.UserType == Roll.Admin)
                {
                    //Create 'Admin' role
                    if (await _roleManager.FindByNameAsync(Roll.Admin.ToString()) is null)
                    {
                        ApplicationRole applicationRole = new ApplicationRole() { Name = Roll.Admin.ToString() };
                        await _roleManager.CreateAsync(applicationRole);
                    }

                    //Add the new user into 'Admin' role
                    await _userManager.AddToRoleAsync(user, Roll.Admin.ToString());
                }
                else
                {
                    //Create 'User' role
                    if (await _roleManager.FindByNameAsync(Roll.Owner.ToString()) is null)
                    {
                        ApplicationRole applicationRole = new ApplicationRole() { Name = Roll.Owner.ToString() };
                        await _roleManager.CreateAsync(applicationRole);
                    }
                    //Add the new user into 'User' role
                    await _userManager.AddToRoleAsync(user, Roll.Owner.ToString());
                }
                //Sign in -create Token -false> do not keep token it....
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "HomeController");
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("Register", error.Description);
                }

                return View(registerDTO);
            }
        }
        #endregion
    
        public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(true); //valid
            }
            else
            {
                return Json(false); //invalid
            }
        }
        
    }

}
