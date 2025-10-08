using Microsoft.Extensions.Configuration;

namespace Config.ConfigReaders
{
    public static class EndPointsConfigReader
    {
        private static IConfigurationRoot? configuration;

        public static void Init(string jsonFilePath)
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory, "Resources"))
                .AddJsonFile(jsonFilePath, optional: false, reloadOnChange: false)
                .AddEnvironmentVariables() // позволяет переопределять через env vars
                .Build();
        }

        public static string Get(string key, string defaultValue = "")
        {
            if (configuration == null) throw new InvalidOperationException("Configuration not initialized");
            var value = configuration[key];
            return string.IsNullOrEmpty(value) ? defaultValue : value;
        }
    }
}