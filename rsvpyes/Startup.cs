using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using rsvpyes.Data;
using rsvpyes.Services;
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
            services.AddTransient<IDataService<RsvpRequest>, DataService<RsvpRequest>>();
            services.AddTransient<IDataService<RsvpResponse>, DataService<RsvpResponse>>();

            // メールサービス
            services.AddSingleton<IMailConfiguration>(new MailConfiguration("smtp7.gmoserver.jp", 25, "y-nagano@i-t-p.co.jp", "Itp201301", @"=======================================
永野有音 <y-nagano@i-t-p.co.jp>", "http://" + WindowsIdentity.GetCurrent().Name + "/response/respond"));
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
