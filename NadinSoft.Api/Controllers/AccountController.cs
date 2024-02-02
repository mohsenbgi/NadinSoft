using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NadinSoft.Api.Models;
using NadinSoft.Infra.Identity;
using NadinSoft.Infra.Identity.Model;

namespace NadinSoft.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : ApiController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppJwtSettings _appJwtSettings;

        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<AppJwtSettings> appJwtSettings,
            ISender sender) : base(sender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appJwtSettings = appJwtSettings.Value;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ResponseResult> Register(RegisterUser registerUser)
        {
            if (!ModelState.IsValid)
            {
                var message = ModelState.Values.SelectMany(v => v.Errors)
                    .First(e => !string.IsNullOrWhiteSpace(e.ErrorMessage))
                    .ErrorMessage;

                return ResponseResult.Failure(message);
            }

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                return ResponseResult.Success("Success", GetFullJwt(user.Email));
            }

            return ResponseResult.Failure(result.Errors.FirstOrDefault().Description);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ResponseResult> Login(LoginUser loginUser)
        {
            if (!ModelState.IsValid)
            {
                var message = ModelState.Values.SelectMany(v => v.Errors)
                    .First(e => !string.IsNullOrWhiteSpace(e.ErrorMessage))
                    .ErrorMessage;

                return ResponseResult.Failure(message);
            }

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                var fullJwt = GetFullJwt(loginUser.Email);
                return ResponseResult.Success("Success", fullJwt);
            }

            if (result.IsLockedOut)
            {
                return ResponseResult.Failure("This user is temporarily blocked");
            }

            return ResponseResult.Failure("Incorrect user or password");
        }

        private string GetFullJwt(string email)
        {
            return new JwtBuilder()
                .WithUserManager(_userManager)
                .WithJwtSettings(_appJwtSettings)
                .WithEmail(email)
                .WithJwtClaims()
                .WithUserClaims()
                .WithUserRoles()
                .BuildToken();
        }
    }
}
