namespace WalletService;
using MassTransit;
using System.Reflection;
public static class ConfigurationService
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddMassTransit(m =>
        {
            m.AddConsumers(Assembly.GetExecutingAssembly());
            m.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("b-e6da7d3b-c83d-4436-af6f-09e83fe7e80d.mq.eu-north-1.amazonaws.com", 5671, "/", c =>
                {
                    c.Username("copartner");
                    c.Password("Cop@rtn$r%123");
                });
                cfg.ConfigureEndpoints(ctx);
            });
        });
        return services;
    }
}