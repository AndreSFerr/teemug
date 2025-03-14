using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;
using TeeMugShop.Application;
using TeeMugShop.Application.Common.Interfaces;
using TeeMugShop.Domain.Entities.Application;
using TeeMugShop.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// Services Configuration
// ==========================================

var configuration = builder.Configuration;
var services = builder.Services;

// Database
//services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseMySql(
//        configuration.GetConnectionString("DefaultConnection"),
//        ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))
//));

services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");

    int retry = 0;
    int maxRetry = 10;
    bool connected = false;
    while (!connected && retry < maxRetry)
    {
        try
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            connected = true;
        }
        catch (MySqlConnector.MySqlException)
        {
            retry++;
            Console.WriteLine($"[Retry {retry}] Aguardando MySQL subir...");
            Thread.Sleep(5000); // 5 segundos
        }
    }

    if (!connected)
    {
        throw new Exception("Não foi possível conectar ao banco de dados após várias tentativas.");
    }
});


// Clean Architecture: DbContext via interface
services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

// Identity configuration com ApplicationUser e ApplicationRole
services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Authentication com redes sociais
services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
})
.AddCookie()
.AddGoogle(options =>
{
    options.ClientId = configuration["Authentication:Google:ClientId"]
        ?? throw new InvalidOperationException("Missing Google ClientId in configuration.");
    options.ClientSecret = configuration["Authentication:Google:ClientSecret"]
        ?? throw new InvalidOperationException("Missing Google ClientSecret in configuration.");
})
.AddFacebook(options =>
{
    options.AppId = configuration["Authentication:Facebook:AppId"]
        ?? throw new InvalidOperationException("Missing Facebook AppId in configuration.");
    options.AppSecret = configuration["Authentication:Facebook:AppSecret"]
        ?? throw new InvalidOperationException("Missing Facebook AppSecret in configuration.");
});

// Swagger
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

// Application Layer
services.AddApplication();

// Inicializador do banco
services.AddScoped<ApplicationDbContextInitialiser>();

// Controllers
services.AddControllers();

// CORS
services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "TeeMugShop API", Version = "v1" });
    c.AddServer(new OpenApiServer
    {
        Url = "http://localhost:8080"
    });
});


var app = builder.Build();

// ==========================================
// Pipeline
// ==========================================

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();

    // Inicializar banco: cria roles e admin se não existirem
    var dbInitializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
    await dbInitializer.InitialiseAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
