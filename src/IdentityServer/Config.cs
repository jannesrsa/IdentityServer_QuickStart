// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;

namespace IdentityServer
{
    public class Config
    {
        private readonly List<Client> _clients = new List<Client>();

        private readonly IConfiguration _config;

        public Config(IConfiguration config)
        {
            _config = config;

            var clients = new List<Client>();
            IConfigurationSection configClients = _config.GetSection("Clients");
            foreach (var configClient in configClients.GetChildren())
            {
                var siteUri = configClient.GetValue<string>("SiteUri").TrimEnd('/');
                this.Clients.Add(new Client
                {
                    ClientId = configClient.GetValue<string>("ClientId"),
                    AllowedGrantTypes = { configClient.GetValue<string>("AllowedGrantTypes") },
                    RedirectUris =
                        {
                            $"{siteUri}/designer/account/signin-oidc",
                            $"{siteUri}/runtime/account/signin-oidc"
                        },
                    PostLogoutRedirectUris =
                        {
                            $"{siteUri}/designer/account/signout-callback-oidc",
                            $"{siteUri}/runtime/account/signout-callback-oidc"
                        },
                    AllowedScopes = configClient.GetSection("AllowedScopes").Get<string[]>(),
                    RequireConsent = false,
                    AlwaysSendClientClaims = true,
                    IdentityTokenLifetime = GetIdentityTokenLifetime(configClient)
                });
            }
        }

        public IEnumerable<ApiResource> Apis =>
           new List<ApiResource>
        {
            new ApiResource("api1", "My API")
        };

        public List<Client> Clients
        {
            get
            {
                return _clients;
            }
        }

        public IEnumerable<IdentityResource> Ids =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };

        private static int GetIdentityTokenLifetime(IConfigurationSection configClient)
        {
            return configClient.GetValue<int>("IdentityTokenLifetime");
        }

        //     var clients = new List<Client>();
        //    return clients;
        //}

        //    {
        //        new Client
        //        {
        //            ClientId = "client",

        //            // no interactive user, use the clientid/secret for authentication
        //            AllowedGrantTypes = GrantTypes.ClientCredentials,

        //            // secret for authentication
        //            ClientSecrets =
        //            {
        //                new Secret("secret".Sha256())
        //            },

        //            // scopes that client has access to
        //            AllowedScopes = { "api1" }
        //        },
        //        // interactive ASP.NET Core MVC client
        //        new Client
        //        {
        //            ClientId = "mvc",
        //            ClientSecrets = { new Secret("secret".Sha256()) },

        //            AllowedGrantTypes = GrantTypes.Code,
        //            RequireConsent = false,
        //            RequirePkce = true,

        //            // where to redirect to after login
        //            RedirectUris = { "http://localhost:5002/signin-oidc" },

        //            // where to redirect to after logout
        //            PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

        //            AllowedScopes = new List<string>
        //            {
        //                IdentityServerConstants.StandardScopes.OpenId,
        //                IdentityServerConstants.StandardScopes.Profile
        //            }
        //        },
        //        // interactive ASP.NET Core MVC client
        //        new Client
        //        {
        //            ClientId = "DpRd/KpD/+rhiwIKUmY++pmjbodV6fS/UfQ29ekA/ow=",
        //           //ClientSecrets = { new Secret("SFDesignerSecret".Sha256()) },

        //            AllowedGrantTypes = GrantTypes.Implicit,

        //            RequireConsent = false,
        //            //RequirePkce = true,//should be set

        //            RedirectUris = { "https://m365x274230.onk2qa.com/designer/account/signin-oidc" },

        //            // where to redirect to after logout
        //            PostLogoutRedirectUris = { "https://m365x274230.onk2qa.com/designer/account/signout-callback-oidc" },

        //            AllowedScopes = new List<string>
        //            {
        //                IdentityServerConstants.StandardScopes.OpenId,
        //                IdentityServerConstants.StandardScopes.Profile,
        //                IdentityServerConstants.StandardScopes.Email
        //            }
        //        }

        //    };
    }
}