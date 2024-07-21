namespace AppointmentManager.Domain.DBModel
{
  public class Appointment
  {
    public int AppointmentId { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Description { get; set; }
    public int? UserId { get; set; } 
    public User User { get; set; } 
  }

}
