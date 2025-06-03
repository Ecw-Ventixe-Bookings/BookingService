using Application.Services;
using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Security.Cryptography;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


var jwtIssuer = builder.Configuration["JWT:Issuer"] ?? throw new NullReferenceException("JWT Issuer is not present");
var jwtAudience = builder.Configuration["JWT:Audience"] ?? throw new NullReferenceException("JWT Audience is not present");
var jwtKeyBase64 = builder.Configuration["JWT:PublicKey"] ?? throw new NullReferenceException("JWT Public key is not present");


builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.WriteIndented = true;
        opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddOpenApi();

builder.Services.AddHttpClient<BookingService>();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<EmailService>();


builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        RSA rsa = RSA.Create();
        byte[] publicKeyBytes = Convert.FromBase64String(jwtKeyBase64);
        rsa.ImportRSAPublicKey(publicKeyBytes, out _);

        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            IssuerSigningKey = new RsaSecurityKey(rsa)
        };
    });

var app = builder.Build();
app.MapOpenApi();
app.MapScalarApiReference("/api/docs");
app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
