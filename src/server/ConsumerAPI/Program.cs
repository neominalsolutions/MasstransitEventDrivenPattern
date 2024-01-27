using ConsumerAPI.Consumers;
using MassTransit;
using Messaging.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(config =>
{
  config.AddConsumer<TaskAssigmentConsumer>();

  config.UsingRabbitMq((context, config) =>
  {
    config.Host(builder.Configuration.GetConnectionString("RabbitMq"));
    config.ReceiveEndpoint(RabbitMqSettings.TaskAssignedQueue, e =>
    {
      // rabbitmq context IBusRegisterationContext i�in consumer tan�m� yapt�k.
      e.ConfigureConsumer<TaskAssigmentConsumer>(context);
    });
  });

});

// MASSTRANSIT 8.0 sonras� art�k bu ayara ihtiya� kalmad�
//builder.Services.AddMassTransitHostedService();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
