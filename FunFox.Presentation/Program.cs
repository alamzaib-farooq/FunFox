using FunFox.Infrastructure.Common;
using FunFox.Infrastructure.Extensions;
using FunFox.Web.Configurations;
using Serilog;

StaticLogger.EnsureInitialized();
Log.Information("Server Booting Up...");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.AddConfigurations();
    builder.UseSerilogLogger(builder.Host);
    builder.Services.AddInfrastructureLayer(builder.Configuration);
    var app = builder.Build();

    await app.Services.SeedUsersAndRoles();

    app.UseExceptionHandlers(app.Environment);
    app.UseInfrastructure();
    app.MapEndpoints();

    app.Run();
}
catch (Exception ex) when (!ex.GetType().Name.Equals("HostAbortedException", StringComparison.Ordinal))
{
    StaticLogger.EnsureInitialized();
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    StaticLogger.EnsureInitialized();
    Log.Information("Server Shutting down...");
    Log.CloseAndFlush();
}
