// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
             new List<ApiScope>
             {
                 new ApiScope("api1", "My API")
             };
        public static IEnumerable<Client> Clients =>
             new List<Client>
             {
                 new Client
                 {
                     ClientId = "client",
                     AllowedGrantTypes = GrantTypes.ClientCredentials,
                     ClientSecrets =
                     {
                         new Secret("secret".Sha256())
                     },
                     // scopes that client has access to
                     AllowedScopes = { "api1" }
                 },
                 // interactive ASP.NET Core MVC client
                 new Client
                 {
                     ClientId = "mvcc",
                     ClientSecrets = { new Secret("secret".Sha256()) },

                     AllowedGrantTypes = GrantTypes.Code,

                     // where to redirect to after login
                     RedirectUris = { "https://localhost:7143/signin-oidc" },

                     // where to redirect to after logout
                     PostLogoutRedirectUris = { "https://localhost:7143/signout-callback-oidc" },

                     AllowedScopes = new List<string>
                     {
                         IdentityServerConstants.StandardScopes.OpenId,
                         IdentityServerConstants.StandardScopes.Profile
                     }
                 },
                 new Client
                 {
                     ClientId = "mvc",
                     ClientSecrets = { new Secret("secret".Sha256()) },
                 
                     AllowedGrantTypes = GrantTypes.Code,
                 
                     // where to redirect to after login
                     RedirectUris = { "https://localhost:7143/signin-oidc" },
                 
                     // where to redirect to after logout
                     PostLogoutRedirectUris = { "https://localhost:7143/signout-callback-oidc" },
                 
                     AllowOfflineAccess = true,
                 
                     AllowedScopes = new List<string>
                     {
                         IdentityServerConstants.StandardScopes.OpenId,
                         IdentityServerConstants.StandardScopes.Profile,
                         "api1"
                     }
                 }
             };
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
    }
}