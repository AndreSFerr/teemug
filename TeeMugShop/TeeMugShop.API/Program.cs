using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))
    ));

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

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
