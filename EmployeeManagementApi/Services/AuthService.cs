using EmployeeManagementApi.DTO;
using EmployeeManagementApi.Exceptions;
using EmployeeManagementApi.Repository;
using EmployeeManagementApi.Repository.Interfaces;
using EmployeeManagementApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManagementApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthService(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        public async Task RegisterUser(RegisterUserRequest registerUserRequest)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerUserRequest.Email.Split('@')[0],
                Email = registerUserRequest.Email,
            };

            var result = await userManager.CreateAsync(
                identityUser,
                registerUserRequest.Password);

            var errors = result.Errors.ToList();

            if (!result.Succeeded)
            {
                if (errors.Any(e =>
            e.Code == "DuplicateUserName" ||
            e.Code == "DuplicateEmail"))
                {
                    throw new ApiException(409, "User already exists");
                } else
                {
                    throw new ApiException(400, "Failed to register user");
                }
            }

            var roleResult = await userManager.AddToRoleAsync(
        identityUser,
        "Employee");

            if (!roleResult.Succeeded)
            {
                await userManager.DeleteAsync(identityUser);

                throw new ApiException(500, "Failed to assign user role");
            }
        }

        public async Task<string> LoginUser(LoginUserRequest loginUserRequest)
        {
            var user = await userManager.FindByEmailAsync(loginUserRequest.Email);

            if (user == null)
            {
                throw new ApiException(404, "User not found");
            }

            var isPasswordCorrect = await userManager.CheckPasswordAsync(user, loginUserRequest.Password);

            if (!isPasswordCorrect)
            {
                throw new ApiException(400, "Failed to login user");
            }

            return await tokenRepository.CreateJwtToken(user);
        }
    }
}
