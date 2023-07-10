using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Library.Models;
using Library.Repositories.Abstract;
using Library.Repositories.Implementation;
using Library.Services;
using Microsoft.AspNetCore.Identity;
using Library.Models.Domain;
using System;


namespace Library
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddScoped<ILoginService>(provider => new LoginService(connectionString));
            services.AddHttpContextAccessor();
            services.AddControllersWithViews();

            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IPublisherService, PublisherService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IRegistrationService, RegistrationService>();

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Login";
                options.AccessDeniedPath = "/Home/AccessDenied";
            });

            services.AddSession(options =>
            {
                options.Cookie.Name = "Library.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(20);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "registration",
                    pattern: "Registration/{action=Registration}/{id?}",
                    defaults: new { controller = "Registration" });

                endpoints.MapControllerRoute(
                    name: "login",
                    pattern: "login/login",
                    defaults: new { controller = "Login", action = "Login" });
                    endpoints.MapControllerRoute(
                    name: "genre",
                    pattern: "genre/getall",
                    defaults: new { controller = "genre", action = "getall" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
