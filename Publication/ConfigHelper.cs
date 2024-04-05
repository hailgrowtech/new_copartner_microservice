
using Microsoft.Extensions.Configuration;


namespace Publication;

    public static class ConfigHelper
    {
    public static AppSettings GetAppSettings()
    {
        var _sections = GetConfigurationSections();
        var _appSettings = _sections.Get<AppSettings>();
        return _appSettings;
    }

    public static IConfigurationSection GetConfigurationSections()
    {
        var _config = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("publicationAppsettings.json").Build();

        var _sections = _config.GetSection(nameof(AppSettings));
        return _sections;
    }
}

