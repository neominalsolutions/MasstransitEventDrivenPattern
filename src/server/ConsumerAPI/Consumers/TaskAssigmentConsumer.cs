using MassTransit;
using Messaging.Events;

namespace ConsumerAPI.Consumers
{
  public class TaskAssigmentConsumer : IConsumer<TaskAssignedEvent>
  {
    public async Task Consume(ConsumeContext<TaskAssignedEvent> context)
    {
      await Console.Out.WriteLineAsync($"{context.Message.TaskId} nolu görev {context.Message.EmployeeId} nolu çalışana {context.Message.EstimatedHour} saatliğine atanmıştır");
    }
  }
}
