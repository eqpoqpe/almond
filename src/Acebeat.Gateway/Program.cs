var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();

IConfigurationSection configuration = !builder.Environment.IsDevelopment() ? builder
           .Configuration
           .AddJsonFile("/etc/almond/acebeat/gateway/appsettings.json")
           .Build()
           .GetSection("ReverseProxy") : builder
           .Configuration
           .GetSection("ReverseProxy");

builder.Services.AddReverseProxy().LoadFromConfig(configuration);

var app = builder.Build();

app.UseHttpsRedirection();
app.MapReverseProxy();

app.Run();
