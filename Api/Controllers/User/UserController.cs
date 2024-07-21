using AppointmentManager.Domain.DBModel;
using AppointmentManager.Repositories.EFGetStarted.DataBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentManager.Api.Controllers.User
{

  //פונקציה לעדכון פרטי משתמש

  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateUser(int id)//, [FromBody] User updatedUser)
    {


      //using var db = new AppointmentManagerContext();

      //// Find the user by their ID
      //var user = await db.Users.FindAsync(id);

      //if (user == null)
      //{
      //  return NotFound($"User with ID {id} not found");
      //}

      // user.UserName = updatedUser.UserName;

      // Save changes to the database
      try
      {
        //await db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        return StatusCode(500, "An error occurred while trying to update the user.");
      }

      return NoContent(); // Return 204 No Content if successful
    }




    // POST: api/appointments
    [HttpPost("user")]
    public async Task<ActionResult<Appointment>> Post()//User user)
    {

    //  if (user == null)
    //  {
    //    return BadRequest("User data is null");
    //  }
    //  using var db = new AppointmentManagerContext();

    //  db.Add(user);
    //  db.SaveChanges();

      return null;
    //  //return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
    }


  }

}
