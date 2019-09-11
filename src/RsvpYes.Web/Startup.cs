using IdentityServer4.Models;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RsvpYes.Data;
using RsvpYes.Domain.Meetings;
using RsvpYes.Domain.Places;
using RsvpYes.Domain.Users;
using RsvpYes.Query;
using RsvpYes.Web.Identity;
using System;

namespace RsvpYes.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var dbContext = new RsvpYesDbContext(new DbContextOptionsBuilder<RsvpYesDbContext>()
                .UseInMemoryDatabase(Configuration.GetConnectionString("RsvpYesConnection"))
                .Options);
            services.AddSingleton(dbContext);
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IIdentityRepository, IdentityRepository>();
            services.AddTransient<IPlaceRepository, PlaceRepository>();
            services.AddTransient<IMeetingPlanRepository, MeetingPlanRepository>();
            services.AddTransient<IOrganizationRepository, OrganizationRepository>();
            services.AddTransient<IMeetingsQuery, MeetingsQuery>();
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddUserStore<ApplicationUserStore>()
                .AddRoleStore<ApplicationRoleStore>()
                .AddDefaultTokenProviders();
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryClients(new[] {
                    new Client()
                    {
                        ClientId = "rsvpyes.client",
                        ClientSecrets = new [] { new Secret("secret".Sha256()) },
                        AllowedScopes = { "rsvpyes" },
                        AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                        AllowAccessTokensViaBrowser = true,
                    }
                })
                .AddInMemoryApiResources(new[] {
                    new ApiResource("rsvpyes", "会開催システム API")
                    {
                        Scopes = new [] { new Scope("rsvpyes", "会開催システム API") },
                        ApiSecrets = new [] { new Secret("rsvpyes-secret".Sha256()) }
                    }
                })
                .AddAspNetIdentity<ApplicationUser>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(config =>
            {
                config.AllowAnyOrigin();
            });
            app.UseIdentityServer();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
