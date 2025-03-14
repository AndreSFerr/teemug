using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeeMugShop.Application.Common.Interfaces;
using TeeMugShop.Application;
using TeeMugShop.Domain.Entities.Application;
using TeeMugShop.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))
    ));

services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

// ✅ Identity configurado UMA única vez
services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

// ✅ Application Layer
services.AddApplication();

// ✅ CORS
services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
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


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
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
