using N_Tier.Application.Models;
using N_Tier.Application.Models.User;
using N_Tier.Core.Entities.Identity;

namespace N_Tier.Application.Services;

public interface IUserService
{
    Task<BaseResponseModel> ChangePasswordAsync(Guid userId, ChangePasswordModel changePasswordModel);

    Task<ConfirmEmailResponseModel> ConfirmEmailAsync(ConfirmEmailModel confirmEmailModel);

    Task<CreateUserResponseModel> CreateAsync(CreateUserModel createUserModel);

    Task<LoginResponseModel> LoginAsync(LoginUserModel loginUserModel);

    Task<ApplicationUser> GetRandomLibrarianAsync();
}
