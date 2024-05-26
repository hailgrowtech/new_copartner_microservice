namespace SignInUserService;
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
        string Port = EncryptionHelper.DecryptString(rabbitMqConfig.Port);
        string Username = EncryptionHelper.DecryptString(rabbitMqConfig.Username);
        string Password = EncryptionHelper.DecryptString(rabbitMqConfig.Password);
        {
            services.AddMassTransit(x =>
            {

                x.UsingRabbitMq((ctx, cfg) =>
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
}