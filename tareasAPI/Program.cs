using tareasAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using tareasAPI.Models;
using tareasAPI.Services.interfaces;
using tareasAPI.Services;

var builder = WebApplication.CreateBuilder(args);

//para que muestre los enpoints del controllador
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar el servicio de la base de datos SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("sqliteConnection")));

// Configuración de Identity
builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization(options =>
    {
        // Define una política para los administradores
        options.AddPolicy("RequireAdminRole", policy => policy.RequireRole(UserRoles.Admin));

        // Define una política para los supervisores
        options.AddPolicy("RequireSupervisorRole", policy => policy.RequireRole(UserRoles.Supervisor));

        // Define una política para los empleados
        options.AddPolicy("RequireEmployeeRole", policy => policy.RequireRole(UserRoles.Employee));
    });

builder.Services.AddScoped<IUserManager, CustomUserManager>();
builder.Services.AddScoped<ISignInManager, CustomSignInManager>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IRepository<Tarea>, TareaRepository>();



builder.Services.AddCors(options =>
{
    options.AddPolicy("NewPolicy", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("NewPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();