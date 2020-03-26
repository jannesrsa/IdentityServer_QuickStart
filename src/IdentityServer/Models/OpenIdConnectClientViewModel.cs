using System.ComponentModel.DataAnnotations;

namespace IdentityServer
{
    public class OpenIdConnectClientViewModel
    {
        [Display(Name = "Allowed Grant Types")]
        public string AllowedGrantTypes { get; set; }

        [Display(Name = "Allowed Scopes")]
        public string AllowedScopes { get; set; }

        [Required]
        [Display(Name = "Client ID")]
        public string ClientID { get; set; }

        [Required]
        [Display(Name = "Identity Token Lifetime")]
        public int? IdentityTokenLifetime { get; set; }

        [Required]
        [Display(Name = "Post Logout Redirect Uris (Comma seperated)")]
        public string PostLogoutRedirectUris { get; set; }

        [Required]
        [Display(Name = "Redirect Uris (Comma seperated)")]
        public string RedirectUris { get; set; }
    }
}