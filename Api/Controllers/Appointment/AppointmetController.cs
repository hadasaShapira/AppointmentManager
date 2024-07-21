using AppointmentManager.Domain.DBModel;
using AppointmentManager.Repositories.EFGetStarted.DataBase;
using AppointmentManager.Repositories.Implementations;
using AppointmentsApp.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AppointmentsController : ControllerBase
{

  //פונקציה לקבלת התור
  //פונקציה לקבלת כל התורים הפנויים
  //פונקציה לעדכון תור
  //פונקציה למחיקת תור
  //סקודלר ליצירת תורים לחודש הקרוב

  private readonly AppointmentRepository _appointmentRepository;
  public AppointmentsController(AppointmentRepository appointmentRepository)
  {
    _appointmentRepository = appointmentRepository;
  }

  // GET: api/appointments/5
  [HttpGet("{id}")]
  [SwaggerOperation(
    Summary = "Get an appointment by its ID",
    Description = "Retrieves the details of a specific appointment by its unique ID."
)]
  public async Task<ActionResult<Appointment>> GetAppointment(int id)
  {

    using var db = new AppointmentManagerContext();

    //גישה דרך ה repository
    // var appointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
    var appointment = await db.Appointments.FindAsync(id);
    if (appointment == null)
    {
      return NotFound();
    }

    return appointment;
  }

  //צפיה בתור לפי ת.ז של משתמש
  // GET: api/appointments/user/{userId}
  [HttpGet("user/{userId}")]

  public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsByUser(int userId)
  {
    using var db = new AppointmentManagerContext();

    // Find all appointments for the user
    var appointments = await db.Appointments
        .Where(a => a.UserId == userId)
        .ToListAsync();

    if (appointments.Count == 0)
    {
      return NotFound("No appointments found for the user.");
    }

    return Ok(appointments);
  }

  // עדכון תור
  // PUT: api/appointments/{id}
  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateAppointment(int id, [FromBody] UpdateAppointmentRequest request)
  {
    if (request == null || string.IsNullOrWhiteSpace(request.Description))
    {
      return BadRequest("Invalid request data.");
    }

    using var db = new AppointmentManagerContext();
    var appointment = await db.Appointments.FindAsync(id);

    if (appointment == null)
    {
      return NotFound("Appointment not found.");
    }

    // Update the appointment's description
    appointment.Description = request.Description;

    try
    {

      // Save changes to the database
      await db.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      return StatusCode(500, "An error occurred while updating the appointment.");
    }

    return NoContent(); // Return 204 No Content if successful
  }

  // Define the request model for updating an appointment
  public class UpdateAppointmentRequest
  {
    public string Description { get; set; }
  }



  // POST: api/appointments
  [HttpPost]
  public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
  {
    using var db = new AppointmentManagerContext();

    db.Add(new Test() { ID = 1986 });
    db.SaveChanges();

    return null;
  }



  // DELETE: Deleting a user appointments
  [HttpDelete("by-user/{userId}")]
  public async Task<IActionResult> DeleteAppointmentsUser(int userId)
  {

    using var db = new AppointmentManagerContext();

    // Find all appointments for the user
    var appointments = await db.Appointments
        .Where(a => a.UserId == userId)
        .ToListAsync();

    if (appointments.Count == 0)
    {
      return NotFound("No appointments found for the user.");
    }

    // Remove the appointments from the context
    db.Appointments.RemoveRange(appointments);

    try
    {
      // Save changes to the database
      await db.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      return StatusCode(500, "An error occurred while trying to delete appointments."); // Return 500 if there's an error
    }

    return NoContent(); // Return 204 No Content if successful
  }

  //scedular to add appointment
  //[HttpPost("add")]
  //public async Task<ActionResult> AddGeneralAppointments([FromQuery] string description, [FromQuery] DateTime? startDate, [FromQuery] int count)
  //{

  //  using var db = new AppointmentManagerContext();

  //  // Validate count
  //  if (count <= 0)
  //  {
  //    return BadRequest("Count must be greater than zero.");
  //  }

  //  var actualStartDate = startDate ?? DateTime.UtcNow.Date;


  //  // Generate appointments
  //  var appointments = new List<Appointment>();
  //  for (int i = 1; i <= count; i++)
  //  {
  //    var user = new User();
  //    user.UserId = i;
  //    user.UserName = "Sara levi";
  //    appointments.Add(new Appointment
  //    {
  //      AppointmentDate = actualStartDate.AddDays(i),
  //      Description = $"{description} {i}",
  //      UserId = user.UserId,
  //      User = user
  //    });
  //  }

  //  // Add appointments to database
  //  db.Appointments.AddRange(appointments);
  //  await db.SaveChangesAsync();

  //  return Ok($"{count} appointments added successfully.");
  //}


}
