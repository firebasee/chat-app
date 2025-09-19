using CompanyChatService.Infrastructure;
using CompanyChatService.WebAPI.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Infrastructure katmanındaki servisleri ekle
builder.Services.AddInfrastructureServices(builder.Configuration);

// SignalR servisini ekle
builder.Services.AddSignalR();

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

app.Run();