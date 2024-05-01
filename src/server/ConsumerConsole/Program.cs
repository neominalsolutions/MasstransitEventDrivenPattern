
// Hosting görevi görmesi için yüklenen paket


using ConsumerConsole.Consumers;
using MassTransit;
using Messaging.Events;
using Messaging.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var builder = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
{
  services.AddMassTransit(config =>
  {
    config.AddConsumer<TaskAssigmentConsumer>();

    config.UsingRabbitMq((context, cfg) =>
    {
      cfg.Host(hostContext.Configuration.GetConnectionString("RabbitMq"));

      // Command olarak gönderildiği için Queue belirttik.
      cfg.ReceiveEndpoint(RabbitMqSettings.TaskAssignedQueue, e => e.ConfigureConsumer<TaskAssigmentConsumer>(context));
    });

  });
});

builder.Build().Run();

