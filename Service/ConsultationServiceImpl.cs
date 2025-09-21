using ConsoleAppCms2025.Repository;
using ConsoleAppCms2025.Service;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
    public class ConsultationServiceImpl : IConsultationService
    {
        private readonly string _connectionString =
            System.Configuration.ConfigurationManager.ConnectionStrings["CsWinSql"].ConnectionString;

        public async Task<bool> AddConsultationAsync(int appointmentId, string symptoms, string diagnosis, string notes)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                string query = @"INSERT INTO TblConsultation (AppointmentId, Symptoms, Diagnosis, Notes)
                                 VALUES (@AppointmentId, @Symptoms, @Diagnosis, @Notes)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);
                    cmd.Parameters.AddWithValue("@Symptoms", symptoms);
                    cmd.Parameters.AddWithValue("@Diagnosis", diagnosis);
                    cmd.Parameters.AddWithValue("@Notes", notes);
                    int result = await cmd.ExecuteNonQueryAsync();
                    return result > 0;
                }
            }
        }
    }
}
