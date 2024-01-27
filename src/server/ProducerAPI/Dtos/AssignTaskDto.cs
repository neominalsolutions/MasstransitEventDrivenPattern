namespace ProducerAPI.Dtos
{
  public class AssignTaskDto
  {
    public string EmployeeId { get; set; }
    public string TaskId { get; set; }

    public int EstimatedHour { get; set; }

  }
}
