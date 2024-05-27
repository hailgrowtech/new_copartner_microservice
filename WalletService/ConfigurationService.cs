namespace WalletService;
using CommonLibrary.CommonDTOs;

using CommonLibrary;
using MassTransit;
using System.Reflection;
public static class ConfigurationService
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqConfig = configuration.GetSection("RabbitMqConfig").Get<RabbitMqConfig>();
        string Hostname = EncryptionHelper.DecryptString(rabbitMqConfig.Hostname);
        ushort Port = Convert.ToUInt16(EncryptionHelper.DecryptString(rabbitMqConfig.Port));
        string Username = EncryptionHelper.DecryptString(rabbitMqConfig.Username);
        string Password = EncryptionHelper.DecryptString(rabbitMqConfig.Password);
        services.AddMassTransit(m =>
        {
            m.AddConsumers(Assembly.GetExecutingAssembly());
            m.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(Hostname, Port, "/", c =>
                {
                    c.Username(Username);
                    c.Password(Password);
                });
                cfg.ConfigureEndpoints(ctx);
            });
        });
        return services;
    }
}