// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using WannaWhat.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using WannaWhat.Data;
using WannaWhat.Core.Models;
using Microsoft.AspNetCore.Builder;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;

namespace WannaWhat.IdentityServer
{
    public static class SeedData
    {
        public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<WannaWhatDbContext>(options =>
               options.UseSqlServer(connectionString));

            services.AddIdentity<WannaWhatUser, IdentityRole>()
                .AddEntityFrameworkStores<WannaWhatDbContext>()
                .AddDefaultTokenProviders();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<WannaWhatDbContext>();
                    context.Database.Migrate();

                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<WannaWhatUser>>();
                    var alice = userMgr.FindByNameAsync("alice").Result;
                    if (alice == null)
                    {
                        alice = new WannaWhatUser
                        {
                            UserName = "alice",
                            Email = "AliceSmith@WanaWhat.co.za",
                            EmailConfirmed = true,
                        };
                        var result = userMgr.CreateAsync(alice, "Pass123$").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(alice, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Alice Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Alice"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                            new Claim("location", "No where"),
                            new Claim("userApi.read", "true"),
                            new Claim("userApi.write", "false"),
                            new Claim("userApi.delete", "false"),
                            new Claim("configApi.read", "true"),
                            new Claim("test", "ALICE TEST")

                        }).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Debug("alice created");
                    }
                    else
                    {
                        Log.Debug("alice already exists");
                    }

                    var bob = userMgr.FindByNameAsync("bob").Result;
                    if (bob == null)
                    {
                        bob = new WannaWhatUser
                        {
                            UserName = "bob",
                            Email = "BobSmith@WanaWhat.co.za",
                            EmailConfirmed = true
                        };
                        var result = userMgr.CreateAsync(bob, "Pass123$").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(bob, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Bob Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Bob"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                            new Claim("location", "Lurking Somewhere"),
                            new Claim("userApi.read", "true"),
                            new Claim("userApi.write", "true"),
                            new Claim("userApi.delete", "false"),
                            new Claim("configApi.read", "true"),
                            new Claim("configApi.write", "true"),
                            new Claim("scope2", "scope2"),
                            new Claim("test", "BOBBY TEST")



                        }).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Debug("bob created");
                    }
                    else
                    {
                        Log.Debug("bob already exists");
                    }

                    var admin = userMgr.FindByNameAsync("admin").Result;
                    if (admin == null)
                    {
                        bob = new WannaWhatUser
                        {
                            UserName = "admin",
                            Email = "Admin@WanaWhat.co.za",
                            EmailConfirmed = true
                        };
                        var result = userMgr.CreateAsync(bob, "Pass123$").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(bob, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Sox Masogisi"),
                            new Claim(JwtClaimTypes.GivenName, "Sogis"),
                            new Claim(JwtClaimTypes.FamilyName, "Masogisi"),
                            new Claim(JwtClaimTypes.WebSite, "https://sox.com"),
                            new Claim("location", "EVERYWHERE"),
                            new Claim("userApi.read", "true"),
                            new Claim("userApi.write", "true"),
                            new Claim("userApi.delete", "true"),
                            new Claim("configApi.read", "true"),
                            new Claim("configApi.write", "true"),
                            new Claim("configApi.delete", "true"),
                            new Claim("test", "BOBBY TEST")

                        }).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Debug("Admin created");
                    }
                    else
                    {
                        Log.Debug("Admin already exists");
                    }
                }
            }
        }

        public static void InitializeDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();

                serviceScope.ServiceProvider.GetRequiredService<WannaWhatDbContext>().Database.Migrate();


                if (!context.Clients.Any())
                {
                    foreach (var client in Config.Clients)
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.IdentityResources)
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.Apis)
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }



                if (!context.ApiScopes.Any())
                {
                    foreach (var resource in Config.ApiScopes)
                    {
                        context.ApiScopes.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
