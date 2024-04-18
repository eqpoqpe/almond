var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();

var app = builder.BuildAlmondGatewayApplication();

app.Run();
