namespace FunFox.Web.Configurations
{
    public static class Startup
    {
        public static WebApplicationBuilder AddConfigurations(this WebApplicationBuilder builder)
        {
            const string configurationsDirectory = "Configurations";
            var env = builder.Environment.EnvironmentName;
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"{configurationsDirectory}/logger.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"{configurationsDirectory}/database.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"{configurationsDirectory}/database.{env}.json", optional: true,
                    reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder;
        }
    }
}
