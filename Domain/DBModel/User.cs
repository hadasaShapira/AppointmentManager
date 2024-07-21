namespace AppointmentManager.Domain.DBModel
{
  public class User
  {
    public int UserId { get; set; }
    public string UserName { get; set; }
    public List<Appointment> Appointments { get; set; } = new(); // רשימת התורים של המשתמש
  }
}
