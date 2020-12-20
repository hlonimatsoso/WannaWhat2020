// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using WannaWhat.IdentityServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WannaWhat.IdentityServer.Services;
using WannaWhat.Core.Interfaces;
using IdentityServer4.Services;
using WannaWhat.Data;
using WannaWhat.Core.Models;
using System;
using System.Reflection;

namespace WannaWhat.IdentityServer
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public string ConnectionString => Configuration.GetConnectionString("DefaultConnection");


        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<WannaWhatDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<WannaWhatUser, IdentityRole>()
                .AddEntityFrameworkStores<WannaWhatDbContext>()
                .AddDefaultTokenProviders();

            //var builder = services.AddIdentityServer(options =>
            //{
            //    options.Events.RaiseErrorEvents = true;
            //    options.Events.RaiseInformationEvents = true;
            //    options.Events.RaiseFailureEvents = true;
            //    options.Events.RaiseSuccessEvents = true;

            //    // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
            //    options.EmitStaticAudienceClaim = true;
            //})
            //    .AddInMemoryIdentityResources(Config.IdentityResources)
            //    .AddInMemoryApiScopes(Config.ApiScopes)
            //    .AddInMemoryClients(Config.Clients)
            //    .AddAspNetIdentity<WannaWhatUser>();



            // not recommended for production - you need to store your key material somewhere secure
            //builder.AddDeveloperSigningCredential();

            var migrationsAssembly = typeof(WannaWhatDbContext).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.UserInteraction.LoginUrl = "/Account/Login";
                options.UserInteraction.LogoutUrl = "/Account/Logout";
                options.Authentication = new IdentityServer4.Configuration.AuthenticationOptions()
                {
                    CookieLifetime = TimeSpan.FromHours(10), // ID server cookie timeout set to 10 hoursb
                    CookieSlidingExpiration = true
                };
            })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(ConnectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(ConnectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
                options.EnableTokenCleanup = true;
            })
            .AddAspNetIdentity<WannaWhatUser>()
            .AddDeveloperSigningCredential();

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    // register your IdentityServer with Google at https://console.developers.google.com
                    // enable the Google+ API
                    // set the redirect URI to https://localhost:5001/signin-google
                    options.ClientId = "923984788102-5co1eqq3ehl6ju5qss1pp0jjg9vjao8v.apps.googleusercontent.com";
                    options.ClientSecret = "A5JU6Ms43lpAsGjProvR3s9G";
                });
            services.AddScoped<IRegistrationHelper, RegistrationService>();
            services.AddScoped<IProfileService, WannaWhatProfileService>();

            //services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            //{
            //    builder.WithOrigins("https://localhost:5001")
            //           .AllowAnyMethod()
            //           .AllowAnyHeader()
            //           .AllowCredentials();
            //}));
        }

        public void Configure(IApplicationBuilder app)
        {
            

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseStaticFiles();
            //app.UseCors("MyPolicy");

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            // this will do the initial DB population
            app.MigrateDatabases();

            app.InitializeDatabase();

            bool seed = Configuration.GetSection("Data").GetValue<bool>("Seed");
            
            if (seed)
                app.SeedDatabase();

        }


    }
}