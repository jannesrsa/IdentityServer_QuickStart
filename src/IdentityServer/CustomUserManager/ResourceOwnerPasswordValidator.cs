using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CustomUserManagerRepository.Interfaces;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace IdentityServer.CustomUserManager
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        //repository to get user from db
        private readonly IUserRepository _userRepository;

        public ResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private static string ComputeHash(string rawData)
        {
            using (SHA512 hash = SHA512.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        //this is used to validate your user account with provided grant at /connect/token
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                var cancellationToken = new System.Threading.CancellationToken();
                var canLogin = await _userRepository.FindCheckUserLoginAsync(context.UserName, context.Password);
                if (canLogin)
                {
                    var user = await _userRepository.FindUserAsync(context.UserName, new CancellationToken());
                    if (user != null)
                    {
                        //set the result
                        context.Result = new GrantValidationResult(
                            subject: user.UserName,
                            authenticationMethod: "custom",
                            claims: user.Claims);

                        return;
                    }
                }
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password.");
                return;
            }
            catch (Exception ex)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
            }
        }
    }
}