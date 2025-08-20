using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Laboratorios.Infraestructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly LaboratoriosContext _context;

        public AppointmentRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Appointment>> GetAppointment()
        {
            try
            {
                var appointmentdb = await _context.Appointment.ToListAsync();
                return appointmentdb;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public Appointment InsertAppointment(Appointment appointment)
        {
            try
            {
                _context.Appointment.Add(appointment);
                _context.SaveChanges();

                return appointment;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }

        public Appointment EditAppointment(Appointment appointment)
        {
            try
            {
                _context.Entry(appointment).State = EntityState.Modified;
                _context.SaveChanges();

                return appointment;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
