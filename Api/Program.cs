using Api.Filters;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;
using Application.Author;
using Application.Book;
using Application.Genre;
using Application.Publisher;
using AutoMapper;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers(opts => { opts.Filters.Add(typeof(AppExceptionFilterAttribute)); });

var mapperConfig = new MapperConfiguration(m =>
{
  m.AddProfile(new BookProfile());
  m.AddProfile(new AuthorProfile());
  m.AddProfile(new GenreProfile());
  m.AddProfile(new PublisherProfile());
});
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.Load("Application"));
builder.Services.AddMediatR(Assembly.Load("Application"));

builder.Services.AddDbContext<PersistenceContext>(opt =>
{
  opt.UseSqlServer(config.GetConnectionString("database"),
    sqlopts => { sqlopts.MigrationsHistoryTable("_MigrationHistory", config.GetValue<string>("SchemaName")); });
});

builder.Services.AddHealthChecks().AddSqlServer(config["ConnectionStrings:database"]);

builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

builder.Services.AddPersistence(config).AddDomainServices();

builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new() {Title = "Library Api", Version = "v1"}); });

Log.Logger = new LoggerConfiguration().Enrich.FromLogContext()
  .WriteTo.Console()
  //.WriteTo.File($"Api-{DateTime.Now.Millisecond}.log", rollingInterval: RollingInterval.Day)
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
app.MapControllers();
app.Run();