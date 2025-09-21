using ConsoleAppCms2025.Model;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Repository
{
    public class AppointmentRepositoryImpl : IAppointmentRepository
    {
        private readonly string _connectionString;

        public AppointmentRepositoryImpl()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CsWinSql"].ConnectionString;
        }

        // Get all appointments for a doctor
        public async Task<List<Appointment>> GetAppointmentsByDoctorAsync(int doctorId)
        {
            var appointments = new List<Appointment>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                string query = "SELECT * FROM TblAppointment WHERE DoctorId=@DoctorId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@DoctorId", doctorId);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            appointments.Add(new Appointment
                            {
                                AppointmentId = Convert.ToInt32(reader["AppointmentId"]),
                                TokenNo = reader["TokenNo"].ToString(),
                                PatientMMR = reader["PatientMMR"].ToString(),
                                DoctorId = Convert.ToInt32(reader["DoctorId"]),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                TimeSlot = reader["TimeSlot"].ToString(),
                                ConsultationType = reader["ConsultationType"].ToString(),
                                AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                                IsActive = Convert.ToBoolean(reader["IsActive"]),
                                IsVisited = Convert.ToBoolean(reader["IsVisited"])  // ✅ Added IsVisited
                            });
                        }
                    }
                }
            }

            return appointments;
        }

        // Get a single appointment by ID
        public async Task<Appointment> GetAppointmentByIdAsync(int appointmentId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                string query = "SELECT * FROM TblAppointment WHERE AppointmentId=@AppointmentId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Appointment
                            {
                                AppointmentId = Convert.ToInt32(reader["AppointmentId"]),
                                TokenNo = reader["TokenNo"].ToString(),
                                PatientMMR = reader["PatientMMR"].ToString(),
                                DoctorId = Convert.ToInt32(reader["DoctorId"]),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                TimeSlot = reader["TimeSlot"].ToString(),
                                ConsultationType = reader["ConsultationType"].ToString(),
                                AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                                IsActive = Convert.ToBoolean(reader["IsActive"]),
                                IsVisited = Convert.ToBoolean(reader["IsVisited"])  // ✅ Added IsVisited
                            };
                        }
                    }
                }
            }

            return null;
        }

        // Book a new appointment
        public async Task<Appointment> BookAppointmentAsync(Appointment appointment)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                string query = @"INSERT INTO TblAppointment 
                                 (PatientMMR, DoctorId, UserId, TimeSlot, ConsultationType, AppointmentDate, IsActive, IsVisited)
                                 VALUES (@PatientMMR, @DoctorId, @UserId, @TimeSlot, @ConsultationType, GETDATE(), 1, 0);

                                 SELECT TOP 1 AppointmentId, TokenNo, AppointmentDate, TimeSlot, ConsultationType, IsVisited
                                 FROM TblAppointment
                                 WHERE PatientMMR = @PatientMMR
                                 ORDER BY AppointmentId DESC;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@PatientMMR", appointment.PatientMMR);
                    cmd.Parameters.AddWithValue("@DoctorId", appointment.DoctorId);
                    cmd.Parameters.AddWithValue("@UserId", appointment.UserId);
                    cmd.Parameters.AddWithValue("@TimeSlot", appointment.TimeSlot);
                    cmd.Parameters.AddWithValue("@ConsultationType", appointment.ConsultationType);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            appointment.AppointmentId = Convert.ToInt32(reader["AppointmentId"]);
                            appointment.TokenNo = reader["TokenNo"].ToString();
                            appointment.AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]);
                            appointment.TimeSlot = reader["TimeSlot"].ToString();
                            appointment.ConsultationType = reader["ConsultationType"].ToString();
                            appointment.IsVisited = Convert.ToBoolean(reader["IsVisited"]);  // ✅ Set IsVisited
                        }
                    }
                }
            }

            return appointment;
        }

        // ✅ Mark an appointment as visited
        public async Task<bool> MarkVisitedAsync(int appointmentId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                string query = "UPDATE TblAppointment SET IsVisited = 1 WHERE AppointmentId = @AppointmentId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);
                    int rows = await cmd.ExecuteNonQueryAsync();
                    return rows > 0;
                }
            }
        }
    }
}
