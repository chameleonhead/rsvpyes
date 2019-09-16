using IdentityServer4.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RsvpYes.Application.Meetings;
using RsvpYes.Data;
using RsvpYes.Domain.Meetings;
using RsvpYes.Domain.Places;
using RsvpYes.Domain.Users;
using RsvpYes.Query;
using RsvpYes.Web.Identity;
using RsvpYes.Web.Swashbuckle;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;

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
            services.AddTransient<MeetingPlanCreateCommandHandler>();
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddUserStore<ApplicationUserStore>()
                .AddRoleStore<ApplicationRoleStore>()
                .AddDefaultTokenProviders();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(config =>
                {
                    config.Authority = "https://localhost:44374";
                    config.Audience = "rsvpyes";
                });
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryClients(new[] {
                    new Client()
                    {
                        ClientId = "rsvpyes.client",
                        ClientSecrets = new [] { new Secret("secret".Sha256()) },
                        AllowedScopes = { "rsvpyes" },
                        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "RsvpYes API", Version = "v1" });
                c.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "password",
                    TokenUrl = "/connect/token",
                    Scopes = new Dictionary<string, string>
                    {
                        { "rsvpyes", "RsvpYes API V1" },
                    }
                });
                c.EnableAnnotations();
                c.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "../rsvpyes-web-frontend/build";
            });
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

            app.UseHttpsRedirection();
            app.UseCors();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rsvpyes API V1");
                c.OAuthClientId("rsvpyes.client");
                c.OAuthClientSecret("secret");
            });
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "../rsvpyes-web-frontend";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
