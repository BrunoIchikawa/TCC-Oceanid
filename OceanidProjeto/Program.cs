using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using OceanidProjeto.Data;
using OceanidProjeto.Repositorios.Interface;
using OceanidProjeto.Repositorios;
using System;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 31))
    )
);

// Autenticação com cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Login";
    });

builder.Services.AddScoped<OceanidProjeto.Cookie.Cookie>();
builder.Services.AddScoped<OceanidProjeto.CarrinhoCompra.CookieCarrinhoCompra>();


// Acesso ao contexto HTTP
builder.Services.AddHttpContextAccessor();

// Sessão
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// MVC
builder.Services.AddControllersWithViews();

// Repositórios
builder.Services.AddScoped<ILoginRepositorio, LoginRepositorio>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();   // Autenticação
app.UseSession();          // Ativando a sessão
app.UseAuthorization();    // Autorização

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
