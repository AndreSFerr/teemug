using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeeMugShop.Application;
using TeeMugShop.Application.Common.Interfaces;
using TeeMugShop.Application.Common.Services;
using TeeMugShop.Application.Common.Settings;
using TeeMugShop.Domain.Entities.Application;
using TeeMugShop.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// ===================
// Load .env variables
// ===================
DotNetEnv.Env.Load(); // carrega do arquivo .env na raiz

var configuration = builder.Configuration;

// Substitui com variáveis de ambiente
configuration["ConnectionStrings:DefaultConnection"] = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
configuration["Authentication:JwtSettings:Issuer"] = Environment.GetEnvironmentVariable("JWT_ISSUER");
configuration["Authentication:JwtSettings:Audience"] = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
configuration["Authentication:JwtSettings:SecretKey"] = Environment.GetEnvironmentVariable("JWT_SECRET");
configuration["Authentication:JwtSettings:TokenExpirationHours"] = Environment.GetEnvironmentVariable("JWT_EXPIRATION");

// Bind JWT Settings
builder.Services.Configure<JwtSettings>(configuration.GetSection("Authentication:JwtSettings"));

// ===================
// Configure Services
// ===================
var services = builder.Services;

// Banco de Dados
services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection")))
);

//services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
//{
//    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
//    var connectionString = configuration.GetConnectionString("DefaultConnection");

//    int retry = 0;
//    int maxRetry = 10;
//    bool connected = false;
//    while (!connected && retry < maxRetry)
//    {
//        try
//        {
//            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
//            connected = true;
//        }
//        catch (MySqlConnector.MySqlException)
//        {
//            retry++;
//            Console.WriteLine($"[Retry {retry}] Aguardando MySQL subir...");
//            Thread.Sleep(5000); // 5 segundos
//        }
//    }

//    if (!connected)
//    {
//        throw new Exception("Não foi possível conectar ao banco de dados após várias tentativas.");
//    }
//});

// Identity com ApplicationUser
services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// JWT apenas (sem login social direto via backend)
services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddCookie();

// CORS
services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Swagger
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

// Application Layer
services.AddApplication();
services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
services.AddScoped<ApplicationDbContextInitialiser>();
services.AddScoped<IJwtTokenService, JwtTokenService>();

// Controllers
services.AddControllers();

// ===================
// Pipeline
// ===================
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();

    var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
    await initializer.InitialiseAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
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
