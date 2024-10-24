using backend.Dto;
using backend.Hubs;
using backend.Model;
using backend.Repository;
using backend.Services;
using backend.Services.PriceDropStrategies;
using backend.Settings;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddSignalR();

builder.Services.AddSingleton<IRepository<Bun>, BunRepository>();

builder.Services.AddSingleton<BakingService>();
builder.Services.AddHostedService<ShelfUpdateService>();

builder.Services.AddKeyedTransient<IPriceDropStrategy, DefaultPriceDropStrategy>("Default");
builder.Services.AddKeyedTransient<IPriceDropStrategy, StalePriceDropStrategy>("Stale");
builder.Services.AddKeyedTransient<IPriceDropStrategy, DoublePriceDropStrategy>("Double");

builder.Services.Configure<List<BunSettings>>(builder.Configuration.GetSection("BunSettings"));
builder.Services.Configure<ShelfSettings>(builder.Configuration.GetSection("ShelfSettings"));
builder.Services.Configure<DefaultPriceDropSettings>(builder.Configuration.GetSection("DefaultPriceDropSettings"));
builder.Services.Configure<StalePriceDropSettings>(builder.Configuration.GetSection("StalePriceDropSettings"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.MapPost("/start", async (BakeRequest bakeRequest, BakingService bakingService) => 
{
    await bakingService.Bake(bakeRequest.Count);
});

app.MapHub<ShelfHub>("/shelf");

app.Run();