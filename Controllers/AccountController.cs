using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ExamPortal.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExamPortal.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public UserManager<IdentityUser> UserManager { get; }
        public SignInManager<IdentityUser> SignInManager { get; }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            LoginDTO model = new LoginDTO
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await SignInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = SignInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            LoginDTO loginDTO = new LoginDTO
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await SignInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            //information about user provided by external login provider
            var info = await SignInManager.GetExternalLoginInfoAsync();
            //if information not present
            if (info == null)
            {
                ModelState.AddModelError(String.Empty, "Error loading external login information");
                return View("Login", loginDTO);
            }
            //if user is already present in system[row must present in AspNetUserLogins table and AspNetUsers table]
            var signInResult = await SignInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                //if user have local account[row in AspNetUsers table] 
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                //if user's email is given by external login provider
                if (email != null)
                {
                    //find user by email in AspNetUsers table
                    var user = await UserManager.FindByEmailAsync(email);
                    //if user do not have local account
                    if (user == null)
                    {
                        //make new local account for user
                        user = new IdentityUser
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                        };
                        await UserManager.CreateAsync(user);
                    }
                    //add row in AspNetUserLogins table assuming user have local account
                    await UserManager.AddLoginAsync(user, info);
                    await SignInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                //if email is not provided
                ViewBag.ErrorTitle = $"Email claim not received from : {info.LoginProvider}";
                ViewBag.ErrorMessage = "Please contact support on help@hello.com";
                return View("Error");
            }
        }
    }
}