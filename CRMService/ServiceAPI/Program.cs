using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;
using Serilog.Events;
using bl =BusinessLayer.Utility;
using DAL.Models;
using BusinessLayer.ExceptionHandlers;
using Microsoft.EntityFrameworkCore;
using BusinessLayer.Utility;
using DAL.Repositories;
using BusinessLayer.Logics;
using DAL.Logics;
using DAL;

var builder = WebApplication.CreateBuilder(args);
var Configs = builder.Configuration;
// Add services to the container.
Log.Logger = new LoggerConfiguration()
                 .MinimumLevel.Verbose()
                  .MinimumLevel.Override("Microsoft", LogEventLevel.Fatal)
                  .Enrich.FromLogContext()
                 .WriteTo.File(Configs["Serilog:FileLocation"], rollingInterval: RollingInterval.Day)
                 .WriteTo.Console()
                  .CreateLogger();
builder.Logging.AddSerilog();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(cor =>
cor.AddPolicy(name: MyAllowSpecificOrigins,
builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
})
);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.Configure<bl.AppsettingsConfig>(builder.Configuration.GetSection("Appsettings"));
builder.Services.AddDbContext<CRMDbContext>(opt => opt.UseSqlServer(Configs["AppSettings:DbConnectionString"]));
builder.Services.AddControllers();
builder.Services.AddScoped<ISqlDataHelper,SqlDataHelper>();
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<AuthenticationLogic>();
builder.Services.AddScoped<AuthenticationLogicDAL>();
builder.Services.AddScoped<CustomerLogic>();
builder.Services.AddScoped<CustomerLogicDal>();

builder.Services.AddAuthentication(cfg => {
    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => {
    x.RequireHttpsMetadata = false;
    x.SaveToken = false;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = Configs["JWTToken:Issuer"],
        ValidAudience = Configs["JWTToken:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8
            .GetBytes(Configs["JWTToken:JWT_Secret"])
        ),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();
app.UseExceptionHandler(_=>{ });
app.UseAuthorization();

app.MapControllers();

app.Run();


