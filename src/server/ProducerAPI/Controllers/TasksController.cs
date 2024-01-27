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
    private readonly IPublishEndpoint publishEndpoint;

    public TasksController(ISendEndpointProvider sendEndpointProvider, IPublishEndpoint publishEndpoint)
    {
      this.sendEndpointProvider = sendEndpointProvider;
      this.publishEndpoint = publishEndpoint;
    }

    [HttpPost]
    public async  Task<IActionResult> AssignTask([FromBody] AssignTaskDto assignTaskDto)
    {
      TaskAssignedEvent @event = new TaskAssignedEvent();
      @event.EmployeeId = assignTaskDto.EmployeeId;
      @event.TaskId = assignTaskDto.TaskId;
      @event.EstimatedHour = assignTaskDto.EstimatedHour;

      // kuyruğu söylemeye gerek yok TaskAssignedEvent'e consume olan her servis kullanabilir. event takibi.
      // await this.publishEndpoint.Publish<TaskAssignedEvent>(@event);

      // send kullandığımızda 2 farklı servis dinlerse servisler artası load balancing yapar.
      // aynı instance yük dengelemek amaçlı arka planda operasyonların yükünü dağıtsın istersek send kullanılabilir.Send message göndermek anlamına gelir. Mesaj göndermek bir komutu yerine getirmek demektir. Publish ise çalışan bir komut sonrasında birden fazla servis gerçekleşen olayı dinleyip kendi aksiyonlarını alsınlar diye vardır. 
      var sendEndPoint = await this.sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMqSettings.TaskAssignedQueue}"));
      await sendEndPoint.Send<TaskAssignedEvent>(@event);


      return Ok();
    }
  }
}
