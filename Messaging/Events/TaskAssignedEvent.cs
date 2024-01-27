using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Events
{
  public class TaskAssignedEvent
  {
    public string TaskId { get; set; }
    public int EstimatedHour { get; set; }
    public string EmployeeId { get; set; }

  }
}
