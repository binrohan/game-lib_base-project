using GameLib.API.Extensions;
using GameLib.API.Infrastructure;
using GameLib.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAppDbContext(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<ExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/Error");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// dotnet ef migrations add Initial -p GameLib.Infrastructure\GameLib.Infrastructure.csproj -s GameLib.API\GameLib.API.csproj -o Data/Migrations
// dotnet ef database update -p GameLib.Infrastructure\GameLib.Infrastructure.csproj -s GameLib.API\GameLib.API.csproj
