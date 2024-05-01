using MassTransit;
using Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerConsole.Consumers
{
  public class TaskAssigmentConsumer : IConsumer<AssignTaskCommand>
  {
    private readonly IPublishEndpoint _publishEndpoint;

    public TaskAssigmentConsumer(IPublishEndpoint publishEndpoint)
    {
      _publishEndpoint = publishEndpoint;
    }
    public async Task Consume(ConsumeContext<AssignTaskCommand> context)
    {
      await Console.Out.WriteLineAsync($"{context.Message.TaskId} nolu görev {context.Message.EmployeeId} nolu çalışana {context.Message.EstimatedHour} saatliğine atanmıştır");

      var @event = new TaskAssignedEvent { TaskId = context.Message.TaskId, EmployeeId = context.Message.EmployeeId };


      await _publishEndpoint.Publish(@event);
    }
  }
}
