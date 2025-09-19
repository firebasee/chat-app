using CompanyChatService.Application;
using CompanyChatService.Infrastructure;
using CompanyChatService.WebAPI.Hubs;
using CompanyChatService.WebAPI.Endpoints;
using CompanyChatService.WebAPI.Services;
using CompanyChatService.Application.Common.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Infrastructure katmanındaki servisleri ekle
builder.Services.AddInfrastructureServices(builder.Configuration);

// Application katmanındaki servisleri ekle
builder.Services.AddApplicationServices();

// SignalR servisini ekle
builder.Services.AddSignalR();

// WebAPI katmanında ISignalRService'i kaydet
builder.Services.AddScoped<ISignalRService, SignalRService>();



// API Explorer ve Swagger için servisleri ekle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Hub endpoint'ini haritala
app.MapHub<ChatHub>("/chathub");

// Tüm endpoint'leri haritala
app.MapEndpoints();

app.Run();
