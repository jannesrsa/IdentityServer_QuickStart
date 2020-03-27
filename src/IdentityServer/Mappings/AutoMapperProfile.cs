using System.Collections.Generic;
using AutoMapper;
using IdentityServer.Models;
using IdentityServer4.Models;

namespace IdentityServer.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Client, OpenIdConnectClientViewModel>()
                 .ForMember(dest =>
                    dest.AllowedGrantTypes,
                    opt => opt.MapFrom(src => ToCommaSeperated(src.AllowedGrantTypes)))
                 .ForMember(dest =>
                    dest.AllowedScopes,
                    opt => opt.MapFrom(src => ToCommaSeperated(src.AllowedScopes)))
                  .ForMember(dest =>
                    dest.PostLogoutRedirectUris,
                    opt => opt.MapFrom(src => ToCommaSeperated(src.PostLogoutRedirectUris)))
                  .ForMember(dest =>
                    dest.RedirectUris,
                    opt => opt.MapFrom(src => ToCommaSeperated(src.RedirectUris)));

            CreateMap<OpenIdConnectClientViewModel, Client>()
                 .ForMember(dest =>
                    dest.AllowedGrantTypes,
                    opt => opt.MapFrom(src => FromCommaSeperated(src.AllowedGrantTypes)))
                 .ForMember(dest =>
                    dest.AllowedScopes,
                    opt => opt.MapFrom(src => FromCommaSeperated(src.AllowedScopes)))
                  .ForMember(dest =>
                    dest.PostLogoutRedirectUris,
                    opt => opt.MapFrom(src => FromCommaSeperated(src.PostLogoutRedirectUris)))
                  .ForMember(dest =>
                    dest.RedirectUris,
                    opt => opt.MapFrom(src => FromCommaSeperated(src.RedirectUris)));
        }

        private static string[] FromCommaSeperated(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }

            return s.Split(',', System.StringSplitOptions.RemoveEmptyEntries);
        }

        private static string ToCommaSeperated(IEnumerable<string> s)
        {
            if (s == null)
            {
                return null;
            }

            return string.Join(',', s);
        }
    }
}