using AppSettingsManager.Models;
using Microsoft.Extensions.Options;

namespace AppSettingsManager
{
    public static class ConfigurationExtension
    {
        public static void AddConfiguration<TConfig>(this IServiceCollection services, 
                                                     IConfiguration configuration, 
                                                     string configurationTag = null) where TConfig : class
        {
            if(string.IsNullOrEmpty(configurationTag))
                configurationTag = typeof(TConfig).Name;

            var instance = Activator.CreateInstance<TConfig>();
            new ConfigureFromConfigurationOptions<TConfig>(configuration.GetSection(configurationTag)).Configure(instance);
            services.AddSingleton(instance);
        }
    }
}
