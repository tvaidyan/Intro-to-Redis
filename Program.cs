using redis_explorations.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddStackExchangeRedisCache(options =>
{
    // load this from external config in a realworld app
    // Reminder: Port 5050 is the port that we mapped to, when instantiating the
    // Redis container in Docker
    options.Configuration = "localhost:5050";
});

builder.Services.AddScoped<ICacheHelper, CacheHelper>();

// Configure the HTTP request pipeline.

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
