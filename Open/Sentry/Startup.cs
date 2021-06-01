using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Open.Domain.Country;
using Open.Infra.Country;
using Open.Sentry.Data;
using Open.Sentry.Models;
using Open.Sentry.Services;

namespace Open.Sentry
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services){
            setDatabase(services);
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));




            services.AddDbContext<CountryDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
               

            
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            // Add application services.
            setAuthentication(services);
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
            services.AddScoped<ICountryObjectsRepository, CountryObjectsRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        protected virtual void setAuthentication(IServiceCollection services) { }
        protected virtual void setDatabase(IServiceCollection services) {
            var s = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(s));
            services.AddDbContext<CountryDbContext>(
                options => options.UseSqlServer(s));
            
        }
    }
}
