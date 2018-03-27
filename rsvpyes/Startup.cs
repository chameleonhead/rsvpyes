using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using rsvpyes.Data;
using rsvpyes.Services;

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

            var db = new RsvpContext(
                new DbContextOptionsBuilder<RsvpContext>()
                .UseSqlite(Configuration.GetConnectionString("RsvpDatabase"))
                .Options);
            db.Database.EnsureCreated();
            services.AddSingleton(typeof(RsvpContext), db);
            services.AddSingleton<IRsvpDataService, RsvpDataService>();
            services.AddTransient<IDataService<User>, DataService<User>>();
            services.AddTransient<IDataService<Meeting>, DataService<Meeting>>();
            services.AddTransient<IDataService<RsvpRequest>, DataService<RsvpRequest>>();
            services.AddTransient<IDataService<RsvpResponse>, DataService<RsvpResponse>>();

            services.AddDbContext<RsvpContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("rsvpyesContext")));
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
