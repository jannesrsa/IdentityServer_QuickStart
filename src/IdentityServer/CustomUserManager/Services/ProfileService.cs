using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CustomUserManagerRepository.Interfaces;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace IdentityServer.CustomUserManager
{
    public class ProfileService : IProfileService
    {
        //services
        private readonly IUserRepository _userRepository;

        public ProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //Get user profile date in terms of claims when calling /connect/userinfo
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                var subjectId = context.Subject.GetSubjectId();
                var user = await _userRepository.FindUserAsync(subjectId, new CancellationToken());
                // issue the claims for the user
                if (user != null)
                {
                    context.IssuedClaims = user.Claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
                }
            }
            catch (Exception ex)
            {
                //log your error
            }
        }

        //check if user account is active.
        public async Task IsActiveAsync(IsActiveContext context)
        {
            try
            {
                //get subject from context (set in ResourceOwnerPasswordValidator.ValidateAsync),
                //var userName = context.Subject.Claims.FirstOrDefault(x => x.Type == "preferred_username")?.Value;
                //var user = await _userRepository.FindUserAsync(userName, new CancellationToken());

                //if (user != null)
                //{
                //    if (user.IsActive)
                //    {
                //        context.IsActive = user.IsActive;
                //    }
                //}
                context.IsActive = true;
                await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                //handle error logging
            }
        }
    }
}