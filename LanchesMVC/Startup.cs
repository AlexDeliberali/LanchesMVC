using LanchesMVC.Context;
using LanchesMVC.Models;
using LanchesMVC.Repositories;
using LanchesMVC.Repositories.Interfaces;
using LanchesMVC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LanchesMVC;

public class Startup
{

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        //Incluindo o serviço para identificar usuário e perfil
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        //Registrando os serviços
        services.AddTransient<ILancheRepository, LancheRepository>();
        services.AddTransient<ICategoriaRepository, CategoriaRepository>();
        services.AddTransient<IPedidoRepository, PedidoRepository>();
        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

        //Incluindo a autorização através da politica vinculada ao grupo de usuários admin
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy =>
            {
                policy.RequireRole("Admin");
            });
        });

        //Cria uma instância a cada request
        services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddControllersWithViews();

        //Adicionando os serviços para habilitar o uso do Session
        services.AddMemoryCache();
        services.AddSession();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeedUserRoleInitial seedUserRoleInitial)
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
        //Cria os perfis primeiro
        seedUserRoleInitial.SeedRoles();
        //Cria os usuário e os aloca nos perfis
        seedUserRoleInitial.SeedUsers();

        //Ativando o uso da Session
        app.UseSession();
        //Ativando o uso da Autenticação
        app.UseAuthentication();
        //Ativando o uso da Autorização
        app.UseAuthorization();

        //Criando um endpoint por categoria
        app.UseEndpoints(endpoints =>
        {
            //Definindo o roteamento para utilizar a Area Admin
            endpoints.MapControllerRoute(
              name: "areas",
              pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
            );

            endpoints.MapControllerRoute(
                name: "categoriaFiltro",
                pattern: "Lanche/{action}/{categoria?}",
                defaults: new { Controller = "Lanche", action = "List" });

            endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}