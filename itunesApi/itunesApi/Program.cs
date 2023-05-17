using ItunesApi.Application.BusinesLogic;
using ItunesApi.Application.BusinesLogic.IBusinessLogic;
using ItunesApi.Application.helper;
using ItunesApi.Application.helper.IJwtServices;
using ItunesApi.Infrastructure.Context;
using ItunesApi.Infrastructure.Interface;
using ItunesApi.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddScoped<IScoreRepository, ScoreRepository>();
builder.Services.AddScoped<ISongsRockBL, SongsRockBL>();
builder.Services.AddScoped<ISongsRapBL, SongsRapBL>();
builder.Services.AddScoped<IAccountsBL, AccountsBL>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IScoreBL, ScoreBL>();
builder.Services.AddScoped<ISongsHitsBL, SongsHitsBL>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AccountDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration["AppSettings:Issuer"],
    ValidAudience = builder.Configuration["AppSettings:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]))
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options
    .WithOrigins(new[] { "http://localhost:8080" })
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
);

app.UseHttpsRedirection();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
var scope = app.Services.CreateScope();
scope.ServiceProvider.GetService<AccountDbContext>().Database.Migrate();
app.Run();
