using ConsoleAppCms2025.Model;
using ConsoleAppCms2025.Repository;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ConsoleAppCms2025.Service
{
    /// <summary>
    /// Implements appointment-related operations by delegating to the repository
    /// and handling cross-cutting concerns when required.
    /// </summary>
    public class AppointmentServiceImpl : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepo;
        private readonly string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CsWinSql"].ConnectionString;

        /// <summary>
        /// Creates a new instance of <see cref="AppointmentServiceImpl"/>.
        /// </summary>
        public AppointmentServiceImpl(IAppointmentRepository appointmentRepo)
        {
            _appointmentRepo = appointmentRepo;
        }

        /// <inheritdoc />
        public Task<List<Appointment>> GetAppointmentsByDoctorAsync(int doctorId)
        {
            return _appointmentRepo.GetAppointmentsByDoctorAsync(doctorId);
        }

        /// <inheritdoc />
        public Task<Appointment> GetAppointmentByIdAsync(int appointmentId)
        {
            return _appointmentRepo.GetAppointmentByIdAsync(appointmentId);
        }

        /// <inheritdoc />
        public Task<Appointment> BookAppointmentAsync(Appointment appointment)
        {
            return _appointmentRepo.BookAppointmentAsync(appointment);
        }

        /// <inheritdoc />
        public async Task MarkAsVisitedAsync(int appointmentId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                string query = "UPDATE TblAppointment SET IsVisited = 1 WHERE AppointmentId = @AppointmentId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
