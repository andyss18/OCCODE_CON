using Microsoft.EntityFrameworkCore;
using OCCODE_CON.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Inyección del contexto de SQL Server
builder.Services.AddDbContext<SqlServerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));

// 2. AGREGAR CACHÉ EN MEMORIA (Solución al error de IDistributedCache)
builder.Services.AddDistributedMemoryCache();

// 3. Configurar Sesiones
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 4. Agregar Controladores y Vistas (DEBE IR ANTES DEL BUILD)
builder.Services.AddControllersWithViews();

// ORACLE (Comentado por ahora, pero en su lugar correcto)
//builder.Services.AddDbContext<OracleContext>(options =>
//    options.UseOracle(
//        builder.Configuration.GetConnectionString("OracleConnection")
//    )
//);

// ==========================================
// AHORA SÍ CONSTRUIMOS LA APLICACIÓN
// ==========================================
var app = builder.Build();

// ==========================================
// CONFIGURACIÓN DEL PIPELINE HTTP (app.Use...)
// ==========================================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// IMPORTANTE: El orden de los middlewares es estricto
app.UseSession();
app.UseAuthorization(); // Se eliminó el duplicado que tenías

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();