using Ktsoft.Data;
using Ktsoft.ModelBinders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ktsoft
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var b = WebApplication.CreateBuilder(args);

            var cStr = b.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            b.Services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(cStr));
            b.Services.AddDatabaseDeveloperPageExceptionFilter();
            b.Services.AddDefaultIdentity<IdentityUser>(o => o.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            b.Services.AddControllersWithViews();
            b.Services.AddMvc(options => options.ModelBinderProviders.Insert(0, new DecimalBinderProvider()));

            var app = b.Build();

            if (app.Environment.IsDevelopment()) app.UseMigrationsEndPoint();
            else {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
