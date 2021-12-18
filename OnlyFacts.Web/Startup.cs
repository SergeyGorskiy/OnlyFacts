using Calabonga.AspNetCore.Controllers.Extensions;
using Calabonga.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlyFacts.Web.Data;
using OnlyFacts.Web.Infrastructure.TagHelpers.PagedListTagHelper;

namespace OnlyFacts.Web
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
            services.Configure<IdentityOptions>(config =>
            {
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("OnlyFactsConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddUnitOfWork<ApplicationDbContext>();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAutoMapper(typeof(Startup));
            services.AddCommandAndQueries(typeof(Startup).Assembly);

            services.AddControllersWithViews();

            services.AddTransient<IPagerTagHelperService, PagerTagHelperService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Site/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "index",
                    pattern: "{controller=Facts}/{action=Index}/{tag:regex([a-z�-�])}/{search:regex([a-z�-�])}/{pageIndex:int?}");

                endpoints.MapControllerRoute(
                    name: "index",
                    pattern: "{controller=Facts}/{action=Index}/{tag:regex([a-z�-�])}/{pageIndex:int?}");

                endpoints.MapControllerRoute(
                    name: "index",
                    pattern: "{controller=Facts}/{action=Index}/{pageIndex:int?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Facts}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
