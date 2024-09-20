using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebApplication_0919.Models.EFModels;
using WebApplication_0919.Models.EFModels.Services;

namespace WebApplication_0919
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // 注冊CategoryRepository
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

            // 注冊CategoryService
            builder.Services.AddScoped<CategoryService>();

            //Add services to the container
            builder.Services.AddControllersWithViews();


            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/Members/Login";
                options.LogoutPath = "/Members/Logout";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                // ...可以再添加更多設定
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();//必須寫在UseAuthorization之前,UseRouting之後
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
