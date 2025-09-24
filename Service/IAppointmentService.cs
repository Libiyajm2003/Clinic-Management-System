using ConsoleAppCms2025.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
		/// <summary>
		/// Defines appointment-related operations such as booking and retrieval.
		/// </summary>
		public interface IAppointmentService
		{
			/// <summary>
			/// Returns all appointments for a given doctor.
			/// </summary>
			Task<List<Appointment>> GetAppointmentsByDoctorAsync(int doctorId);

			/// <summary>
			/// Gets a specific appointment by its identifier.
			/// </summary>
			Task<Appointment> GetAppointmentByIdAsync(int appointmentId);

			/// <summary>
			/// Books a new appointment and returns the persisted entity with identifiers.
			/// </summary>
			Task<Appointment> BookAppointmentAsync(Appointment appointment);

			/// <summary>
			/// Marks an appointment as visited after consultation.
			/// </summary>
			Task MarkAsVisitedAsync(int appointmentId);
		}
	}

