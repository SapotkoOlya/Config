using Config.Models;
using Microsoft.Extensions.Configuration;

namespace Config.ConfigReaders
{
    public static class CredentialsConfigReader
    {
        public static UserModel? ReadConfig(string filePath, string sectionName)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory, "Resources"))
                .AddJsonFile(filePath, optional: false, reloadOnChange: false);

            var config = builder.Build();
            var section = config.GetSection(sectionName);
            if (!section.Exists()) return null;
            return section.Get<UserModel>();
        }
    }
}