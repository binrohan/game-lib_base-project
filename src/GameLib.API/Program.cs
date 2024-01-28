using GameLib.API.Extensions;
using GameLib.API.Infrastructure;
using GameLib.Infrastructure.Data;
using GameLib.Core.Exceptions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(opt => opt.Conventions.Add(new RouteTokenTransformerConvention(new KebabCasingTransformer())));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// App Services
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddAppDbContext(builder.Configuration);
builder.Services.AddAppServices();
builder.Services.AddMapperProfiles();
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwaggerDocumentation();

app.UseExceptionHandler("/Error");

app.UseRequestLogger();

app.UsePerformanceMonitor();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
