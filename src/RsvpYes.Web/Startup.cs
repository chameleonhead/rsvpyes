using IdentityServer4.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
using Swashbuckle.AspNetCore.Swagger;
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
            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
