using System.Collections.Generic;
using System.Security.Claims;

namespace CustomUserManagerRepository.Dto
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string DisplayName { get; set; }

        public Claim[] Claims
        {
            get
            {
                var claims = new List<Claim>()
                {
                    new Claim("preferred_username", UserName),
                    new Claim("name", !string.IsNullOrEmpty(DisplayName)?DisplayName:UserName)
                };
                if (!string.IsNullOrEmpty(UserEmail))
                {
                    claims.Add(new Claim("email", UserEmail));
                }
                
                return claims.ToArray();
            }
        }
    }
}