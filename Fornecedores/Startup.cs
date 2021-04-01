using Fornecedores.Services;
using Fornecedores.Models.Contexto;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Fornecedores
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
            services.AddDbContext<Contexto>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews();
            services.AddAuthentication("CookieAuthentication")
                   .AddCookie("CookieAuthentication", config =>
                   {
                       config.Cookie.Name = "UserLoginCookie";
                       config.LoginPath = "/Login/UsuarioLogin";
                       config.LogoutPath = "/Fornecedor/Logout";
                       config.AccessDeniedPath = "/Login/AccessDenied";
                   });
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, AuthMessageSender>();

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

            // Quem é você?
            app.UseAuthentication();

            // Verifica Permissões
            app.UseAuthorization();      

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=UsuarioLogin}/{id?}");
            });
        }
    }
}
