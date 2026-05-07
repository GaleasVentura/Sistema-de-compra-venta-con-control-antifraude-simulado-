using Microsoft.EntityFrameworkCore;
using PerfumeriaMVC.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// ✅ DB CONTEXT
builder.Services.AddDbContext<Perfumeriadb>(options =>
    options.UseSqlServer(
        "Server=localhost\\SQLEXPRESS;Database=Perfumeria_DB;Trusted_Connection=True;TrustServerCertificate=True;"
    ));

// ✅ SESSION (CORRECTO)
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

// ⚠️ IMPORTANTE: SESSION antes de MapControllerRoute
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();