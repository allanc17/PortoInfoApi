using PortoInfoApi.Data;
using PortoInfoApi.Interfaces;
using PortoInfoApi.Repository;
using PortoInfoApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);

// --- Configuração EF Core e Injeção de Dependência ---
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("UserDatabase"));

// CORRIGIDO: O sistema agora sabe onde achar IUserRepository e UserService
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();

// --- Configuração JWT (continua a mesma) ---
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddControllers();
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ====================================================================

builder.Services.AddControllers();
builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Habilita a interface gráfica que você precisa acessar
}
// SE VOCÊ AINDA TIVER PROBLEMAS, PODE TIRAR O IF TEMPORARIAMENTE.

// ========================================================================

// REMOVER OU COMENTAR TEMPORARIAMENTE: Evita o erro de redirecionamento HTTPS na porta 5136
// app.UseHttpsRedirection(); 

// Middleware de segurança (a ordem é importante!)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();