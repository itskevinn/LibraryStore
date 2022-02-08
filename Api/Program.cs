using Api.Filters;
using Application.Library.Author;
using Application.Library.Book;
using Application.Library.Genre;
using Application.Library.Publisher;
using Application.Security.Role;
using Application.Security.User;
using AutoMapper;
using Infrastructure.Extensions;
using Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Application.Config;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers(opts => { opts.Filters.Add(typeof(AppExceptionFilterAttribute)); });

var mapperConfig = new MapperConfiguration(m =>
{
  m.AddProfile(new BookProfile());
  m.AddProfile(new AuthorProfile());
  m.AddProfile(new GenreProfile());
  m.AddProfile(new PublisherProfile());
  m.AddProfile(new RoleProfile());
  m.AddProfile(new UserProfile());
  m.AddProfile(new MenuItemProfile());
  m.AddProfile(new BrandInfoProfile());
});
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  var securityScheme = new OpenApiSecurityScheme
  {
    Name = "JWT Authentication",
    Description = "Enter JWT Bearer token **_only_**",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.Http,
    Scheme = "bearer",
    BearerFormat = "JWT",
    Reference = new OpenApiReference
    {
      Id = JwtBearerDefaults.AuthenticationScheme,
      Type = ReferenceType.SecurityScheme,
    }
  };
  c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
  c.AddSecurityRequirement(new OpenApiSecurityRequirement
  {
    {securityScheme, new string[] { }}
  });
});
builder.Services.AddAutoMapper(Assembly.Load("Application"));
builder.Services.AddMediatR(Assembly.Load("Application"), typeof(Program).Assembly);
builder.Services.AddDbContext<PersistenceContext>(opt =>
{
  opt.UseSqlServer(config.GetConnectionString("local"),
    sqlOpts => { sqlOpts.MigrationsHistoryTable("_MigrationHistory", config.GetValue<string>("SchemaName")); });
});

builder.Services.AddHealthChecks().AddSqlServer(config["ConnectionStrings:local"]);

builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

builder.Services.AddPersistence(config).AddDomainServices().AddTokenService(config);

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
  opt.Authority = "https://localhost:7150/";
});

builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new() { Title = "Library Api", Version = "v1" }); });

Log.Logger = new LoggerConfiguration().Enrich.FromLogContext()
  .WriteTo.Console()
  .WriteTo.File($"AppLogs/Api-{DateTime.Now.Millisecond}.log", rollingInterval: RollingInterval.Day)
  .CreateLogger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library Api"));
}

app.UseRouting();
app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.Run();