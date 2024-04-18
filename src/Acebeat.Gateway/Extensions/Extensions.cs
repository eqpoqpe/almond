internal static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        const string configureFilePath = "/etc/almond/gateway/appsettings.json";

        if (!builder.Environment.IsDevelopment() && !File.Exists(configureFilePath))
            Utilities.CopyConfigureFile("./appsettings.json", configureFilePath);

        IConfigurationSection configuration = !builder.Environment.IsDevelopment() ? builder
           .Configuration
           .AddJsonFile(configureFilePath)
           .Build()
           .GetSection("ReverseProxy") : builder
           .Configuration
           .GetSection("ReverseProxy");

        builder.Services.AddReverseProxy().LoadFromConfig(configuration);
    }

    public static WebApplication BuildAlmondGatewayApplication(
        this WebApplicationBuilder applicationBuilder)
    {
        var app = applicationBuilder.Build();

        app.UseHttpsRedirection();
        app.MapReverseProxy();

        return app;
    }
}