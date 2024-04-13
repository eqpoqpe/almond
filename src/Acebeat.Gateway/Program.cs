var builder = WebApplication.CreateBuilder(args);

const string configureFilePath = "/etc/almond/gateway/appsettings.json";

if (!builder.Environment.IsDevelopment() && !File.Exists(configureFilePath))
    Utilities.CopyConfigureFile("./appsettings.json", configureFilePath);

builder.AddApplicationServices();

IConfigurationSection configuration = !builder.Environment.IsDevelopment() ? builder
           .Configuration
           .AddJsonFile(configureFilePath)
           .Build()
           .GetSection("ReverseProxy") : builder
           .Configuration
           .GetSection("ReverseProxy");

builder.Services.AddReverseProxy().LoadFromConfig(configuration);

var app = builder.Build();

app.UseHttpsRedirection();
app.MapReverseProxy();

app.Run();
