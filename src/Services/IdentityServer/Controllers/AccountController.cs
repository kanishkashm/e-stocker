using IdentityModel;
using IdentityServer.Data;
using IdentityServer.Model;
using IdentityServer4.Services;
using IdentityServerHost.Quickstart.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EStk.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IIdentityServerInteractionService interaction,
            RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _interaction = interaction;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post(LoginViewModel model)
        {
            // check if we are in the context of an authorization request
            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberLogin, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    if (context != null)
                    {
                        // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                        return Ok("Url Authorized");
                        //return Redirect(model.ReturnUrl);
                    }

                    // request for a local page
                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Ok("Local URL");
                        //return Redirect(model.ReturnUrl);
                    }
                    else if (string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return Ok("No Return URL");
                        //return Redirect("~/");
                    }
                    else
                    {
                        // user might have clicked on a malicious link - should be logged
                        return BadRequest("invalid return URL");
                        throw new Exception("invalid return URL");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid credentials");
            }

            return Unauthorized("Invalid credentials");
            //return View(model);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var clientRequest = HttpContext.Request.Headers["Referer"];
            // This code should be in transaction scope and need to rollback user adding if there is any error after

            try
            {
                // adding user
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    // log goes to here
                    return Conflict(result.Errors.First().Description);
                }
                var userRole = "";
                if(model.Role == 1)
                {
                    userRole = "Admin";
                }
                else if(model.Role == 2)
                {
                    userRole = "User";
                }
                else if(model.Role == 3)
                {
                    userRole = "Auditor";
                }

                await _userManager.AddToRoleAsync(user, userRole);

                //result = await _userManager.AddClaimsAsync(user, new Claim[]{
                //            new Claim(JwtClaimTypes.Email, model.Email),
                //            new Claim(JwtClaimTypes.Role, userRole)
                //        });

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
               // await _userManager.AddToRoleAsync(poweruser, "Admin");
                return Ok("RegistrationSuccess");
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}", ex);
            }
            return Ok();
        }

        
    }
}
