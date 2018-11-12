using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using rsvpyes.Data;
using rsvpyes.Services;
using System;
using System.Security.Principal;

namespace rsvpyes
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
            services.AddMvc();

            services.AddSingleton<IRsvpDataService, RsvpDataService>();
            services.AddTransient<IDataService<User>, DataService<User>>();
            services.AddTransient<IDataService<Meeting>, DataService<Meeting>>();
            services.AddTransient<IDataService<Message>, DataService<Message>>();
            services.AddTransient<IDataService<RsvpRequest>, DataService<RsvpRequest>>();
            services.AddTransient<IDataService<RsvpResponse>, DataService<RsvpResponse>>();

            // メールサービス
            var smtpHost = Configuration["Mail:SmtpHost"];
            var smtpPort = Configuration.GetValue("Mail:SmtpPort", 25);
            var smtpUser = Configuration["Mail:SmtpUser"];
            var smtpPassword = Configuration["Mail:SmtpPassword"];
            var senderSignature = Configuration["Mail:SenderSignature"]?.Replace("\\n", Environment.NewLine);
            services.AddSingleton<IMailConfiguration>(new MailConfiguration(smtpHost, smtpPort, smtpUser, smtpPassword, senderSignature));
            services.AddTransient<IMailService, MailService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
