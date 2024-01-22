using GameLib.API.Extensions;
using GameLib.API.Infrastructure;
using GameLib.Infrastructure.Data;
using GameLib.Core.Exceptions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// App Services
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddAppDbContext(builder.Configuration);
builder.Services.AddAppServices();
builder.Services.AddMapperProfiles();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/Error");

app.UseRequestLogger();

app.UsePerformanceMonitor();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
