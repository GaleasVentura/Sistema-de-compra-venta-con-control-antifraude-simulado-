using Microsoft.EntityFrameworkCore;
using PerfumeriaMVC.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<Perfumeriadb>(options =>
    options.UseSqlServer(
        "Server=localhost\\SQLEXPRESS;Database=Perfumeria_DB;Trusted_Connection=True;TrustServerCertificate=True;"
    ));

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=productos}/{action=Index}/{id?}");

app.Run();