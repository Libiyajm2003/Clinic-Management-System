using ConsoleAppCms2025.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
        public interface IAppointmentService
        {
            Task<List<Appointment>> GetAppointmentsByDoctorAsync(int doctorId);
            Task<Appointment> GetAppointmentByIdAsync(int appointmentId);
            Task<Appointment> BookAppointmentAsync(Appointment appointment);
            Task MarkAsVisitedAsync(int appointmentId); // ✅ Add this
        }
    }

