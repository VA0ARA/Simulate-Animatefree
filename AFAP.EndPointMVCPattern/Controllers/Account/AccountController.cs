using AFAP.EndPointMVCPattern.Infrastructure;
using APAF.Domain.Core.Dtos.Account;
using APAF.Domain.Core.Entities.IdentityEntities;
using APAF.Domain.Core.Enums;
using APAF.Infrastructure.EFCore.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace AFAP.EndPointMVCPattern.Controllers.Account
{
    //[AllowAnonymous]
    public class AccountController : Controller
    {
        #region Prop
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly AppDbContext _appDbContext;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _appDbContext = appDbContext;
        }
        #endregion
        #region Register
        //[Authorize(Roles = "Owner")]
        [CustomAuthorizeAttributeAccess]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        //[Authorize(Roles = "Owner")]
        [CustomAuthorizeAttributeAccess]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
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
               // await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
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
        #region Login
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO loginDTO, string? ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                //ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                return View(loginDTO);
            }
            // create Token  isPersistent: false >> do not keep Token
            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                {
                    return LocalRedirect(ReturnUrl);
                }
                 return RedirectToAction("Index", "Home");

            }
            ModelState.AddModelError("Login", "Inalid email or password");
            return View(loginDTO);
        }
        #endregion
        #region LogOut
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            // expire Token
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        #endregion
        #region EamilQuery
        [AllowAnonymous]
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
        #endregion
        #region Delete Admin
        //[Authorize(Roles = "Owner")]
        [CustomAuthorizeAttributeAccess]
        [HttpGet]
        public async Task<IActionResult> ShowDetailUserPanel()
        {
            var users = await _userManager.Users.Select(c => new UserDTO()
            {
                //Password = c.PasswordHash,
                Email = c.Email,
                Id = c.Id,
                PersonName=c.PersonName,
                Phone=c.PhoneNumber
                //Role = string.Join(",", _userManager.GetRolesAsync(c).Result.ToArray())
            }).ToListAsync();
            return View(users);
        }
        // [Authorize(Roles = "Owner")]
        [CustomAuthorizeAttributeAccess]
        [HttpGet]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            string Id = id.ToString();
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                TempData["error"] = "اطلاعات ثبت نشد دوباره سعی کنید";
                return RedirectToAction("ShowDetailUserPanel");
            }
            else
            {
                await _userManager.DeleteAsync(user);
            }
            return View(user);

        }
        [Authorize(Roles = "Owner")]
        [CustomAuthorizeAttributeAccess]
        [HttpPost]
        public  IActionResult DeleteUser()
        {
            return RedirectToAction("ShowDetailUserPanel");
        }
        #endregion

    }
}
