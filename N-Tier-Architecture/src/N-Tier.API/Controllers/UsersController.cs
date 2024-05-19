using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using N_Tier.Application.Models;
using N_Tier.Application.Models.User;
using N_Tier.Application.Services;
using N_Tier.Core.Entities.Identity;

namespace N_Tier.API.Controllers;

public class UsersController : ApiController
{
    private readonly IUserService _userService;
    private readonly SignInManager<ApplicationUser> _signInManager;

    private readonly UserManager<ApplicationUser> _userManager;

    public UsersController(IUserService userService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        userManager.Options.SignIn.RequireConfirmedAccount = false;
        userManager.Options.SignIn.RequireConfirmedPhoneNumber = false;
        userManager.Options.SignIn.RequireConfirmedEmail = false;

        _userService = userService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterAsync(CreateUserModel createUserModel)
    {
        return Ok(ApiResult<CreateUserResponseModel>.Success(await _userService.CreateAsync(createUserModel)));
    }


    [HttpPost("confirmEmail")]
    public async Task<IActionResult> ConfirmEmailAsync(ConfirmEmailModel confirmEmailModel)
    {
        return Ok(ApiResult<ConfirmEmailResponseModel>.Success(
            await _userService.ConfirmEmailAsync(confirmEmailModel)));
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginPost(LoginUserModel user)
    {
        var loggedInUser = await _userService.LoginAsync(user);

        return Ok(ApiResult<LoginResponseModel>.Success(loggedInUser));

    }
}
