using MassTransit;
using Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerConsole.Consumers
{
  public class TaskAssigmentConsumer : IConsumer<TaskAssignedEvent>
  {
    public async Task Consume(ConsumeContext<TaskAssignedEvent> context)
    {
      await Console.Out.WriteLineAsync($"{context.Message.TaskId} nolu görev {context.Message.EmployeeId} nolu çalışana {context.Message.EstimatedHour} saatliğine atanmıştır");
    }
  }
}
