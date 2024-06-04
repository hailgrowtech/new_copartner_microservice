
using Microsoft.Extensions.Configuration;


namespace Publication;

    public static class ConfigHelper
    {
    public static AppSettings GetAppSettings()
    {
        var _sections = GetConfigurationSection("AppSettings");
        var _appSettings = _sections.Get<AppSettings>();
        return _appSettings;
    }

    public static EmailConfiguration GetEmailSettings(string emailType)
    {
        var section = GetConfigurationSection($"EmailConfigurations:{emailType}");
        var emailSettings = section.Get<EmailConfiguration>();
        return emailSettings;
    }

    //public static IConfigurationSection GetConfigurationSections()
    //{
    //    var _config = new ConfigurationBuilder()
    //       .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    //       .AddJsonFile("publicationAppsettings.json").Build();

    //    var _sections = _config.GetSection(nameof(AppSettings));
    //    return _sections;
    //}

    private static IConfigurationSection GetConfigurationSection(string sectionName)
    {
        var config = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("publicationAppsettings.json")
           .Build();

        var section = config.GetSection(sectionName);
        if (section == null)
        {
            throw new ArgumentException($"Section '{sectionName}' not found in configuration.");
        }

        return section;
    }

    public class EmailConfiguration
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FromDisplayName { get; set; }
    }
}

