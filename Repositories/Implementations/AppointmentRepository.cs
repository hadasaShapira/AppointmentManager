using AppointmentManager.Domain.DBModel;
using AppointmentManager.Repositories.EFGetStarted.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentManager.Repositories.Implementations
{
  public class AppointmentRepository
  {
    private readonly AppointmentManagerContext _context;

    public AppointmentRepository(AppointmentManagerContext context)
    {
      _context = context;
    }

    public async Task<Appointment> GetAppointmentByIdAsync(int id)
    {
      return await _context.Appointments.FindAsync(id);
    }

    public async Task<List<Appointment>> GetAppointmentsByUserIdAsync(int userId)
    {
      return await _context.Appointments
          .Where(a => a.UserId == userId)
          .ToListAsync();
    }

    public async Task<bool> UpdateAppointmentAsync(int id, string description)
    {
      var appointment = await _context.Appointments.FindAsync(id);

      if (appointment == null)
      {
        return false;
      }

      appointment.Description = description;

      try
      {
        await _context.SaveChangesAsync();
        return true;
      }
      catch (DbUpdateConcurrencyException)
      {
        return false;
      }
    }

    public async Task AddAppointmentAsync(Appointment appointment)
    {
      _context.Appointments.Add(appointment);
      await _context.SaveChangesAsync();
    }

    public async Task AddAppointmentsAsync(IEnumerable<Appointment> appointments)
    {
      _context.Appointments.AddRange(appointments);
      await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAppointmentsByUserIdAsync(int userId)
    {
      var appointments = await _context.Appointments
          .Where(a => a.UserId == userId)
          .ToListAsync();

      if (appointments.Count == 0)
      {
        return false;
      }

      _context.Appointments.RemoveRange(appointments);

      try
      {
        await _context.SaveChangesAsync();
        return true;
      }
      catch (DbUpdateConcurrencyException)
      {
        return false;
      }
    }
  }
}
