using Application.Interfaces.Services;
using Infrastructure.AppSettings.Models;
using Infrastructure.Constants;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAppSettingsModels(configuration);

        services.AddAuthentication();
        services.AddAuthorization();

        services.AddScoped<IEmailService, EmailService>();

        return services;
    }

    public static void AddAppSettingsModels(this IServiceCollection services,
    IConfiguration configuration)
    {
        (configuration as ConfigurationManager).AddUserSecrets(Assembly.GetExecutingAssembly(), true);

        var smtpSettingsSection = configuration.GetSection(SettingsSectionNameConstants.SmtpSettings);
        services.Configure<SmtpSettings>(smtpSettingsSection);
    }
}
