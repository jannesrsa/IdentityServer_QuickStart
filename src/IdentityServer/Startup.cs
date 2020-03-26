// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Reflection;
using AutoMapper;
using CustomUserManagerRepository;
using CustomUserManagerRepository.Interfaces;
using IdentityServer.CustomUserManager;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
{
    public class Startup
    {
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // uncomment if you want to add MVC
            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer();

            // uncomment, if you want to add MVC
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // uncomment, if you want to add an MVC-based UI
            services.AddControllersWithViews();
            var config = new Config(Configuration);
            var builder = services.AddIdentityServer()
                .AddInMemoryIdentityResources(config.Ids)
                .AddInMemoryApiResources(config.Apis)
                .AddProfileService<ProfileService>();

            services.AddSingleton<IClientStore, DynamicClientStore>();

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();

            services.AddAutoMapper(options =>
            {
                options.AddProfile<CustomUserManagerRepository.Mappings.AutoMapperProfile>();
                options.AddProfile<Mappings.AutoMapperProfile>();
            }, new Assembly[] { });

            services.AddSingleton<IConnectionProvider, ConnectionProvider>();
            services.AddSingleton<IRepositoryFactory, RepositoryFactory>();

            services.AddTransient(s =>
            {
                var f = s.GetRequiredService<IRepositoryFactory>();
                return f.GetUserRepository();
            });

            // services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddTransient<IProfileService, ProfileService>();
        }
    }
}