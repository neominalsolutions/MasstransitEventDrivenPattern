using MassTransit;
using Messaging.Events;
using Messaging.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProducerAPI.Dtos;

namespace ProducerAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TasksController : ControllerBase
  {
    private readonly ISendEndpointProvider sendEndpointProvider;
    public TasksController(ISendEndpointProvider sendEndpointProvider)
    {
      this.sendEndpointProvider = sendEndpointProvider;
    }

    [HttpPost]
    public async  Task<IActionResult> AssignTask([FromBody] AssignTaskDto assignTaskDto)
    {
      AssignTaskCommand message = new AssignTaskCommand();
      message.EmployeeId = assignTaskDto.EmployeeId;
      message.TaskId = assignTaskDto.TaskId;
      message.EstimatedHour = assignTaskDto.EstimatedHour;

      var sendEndPoint = await this.sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMqSettings.TaskAssignedQueue}"));
      await sendEndPoint.Send(message);


      return Ok();
    }
  }
}
