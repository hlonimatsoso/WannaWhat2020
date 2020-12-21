// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace WannaWhat.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("userInfo",new List<string>{ "gender", "age"}),
                new IdentityResource("userInventory",new List<string>{ "xp", "wincCount","receivedWincCount","heartCount","receivedHeartCount","fakeRabbits","realRabbits"}),
                new IdentityResource("interests",new List<string>{ "boys","girls", "indoors", "outdoors", "sports","boardGames","videoGames",""}),
                new IdentityResource("personality",new List<string>{ "shy","quiet","friednly","rude","outSpoken","arrogant","confident","loud","introvert","extrovert"}),
                new IdentityResource("location",new List<string>{ "location"}),
                new IdentityResource("test",new List<string>{ "test"})

                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("scope1",new string[]{ "scope1"}),
                new ApiScope("scope2",new string[]{ "scope2"}),
                new ApiScope("userApi",new List<string>{
                    "userApi.read",
                    "userApi.write",
                    "userApi.delete"}),
                new ApiScope("configApi",new List<string>{
                    "configApi.read",
                    "configApi.write",
                    "configApi.delete"
                })

            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]{

            new ApiResource("userApi", "User API",new List<string>{"userApi.read","userApi.edit","userApi.delete" })
            {
            Scopes = new List<string>{"userApi","scope1", "scope2" }
            },
            new ApiResource("configApi", "Configuration API",new List<string>{"configApi.read", "configApi.edit", "configApi.delete","scope2" })
            {
            Scopes = new List<string>{"configApi", "scope2" }

            }

            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "blazor",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedCorsOrigins = { "https://localhost:5001" },
                    AllowedScopes = { "openid", "profile", "email","userInfo", "userInventory", "interests","personality","userApi","configApi","location","test","scope1","scope2"},
                    RedirectUris = { "https://localhost:5001/authentication/login-callback" },
                    PostLogoutRedirectUris = { "https://localhost:5001/" },
                    Enabled = true,
                    AlwaysIncludeUserClaimsInIdToken = true

                }
            };
    }
}