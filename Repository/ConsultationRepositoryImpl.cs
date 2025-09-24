using ConsoleAppCms2025.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCmsv2025.Repository
{
    /// <summary>
    /// SQL Server implementation of consultation data access operations.
    /// </summary>
    public class ConsultationRepositoryImpl : IConsultationRepository
    {
        private readonly string _connectionString = @"Server=localhost;Database=cmsv2025db;Trusted_Connection=True;Encrypt=False;";

        /// <inheritdoc />
        public async Task<bool> AddConsultationAsync(int appointmentId, string symptoms, string diagnosis, string notes)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            await con.OpenAsync();

            string query = "INSERT INTO TblConsultation(AppointmentId, Symptoms, Diagnosis, Notes) " +
                           "VALUES(@appointmentId, @symptoms, @diagnosis, @notes)";

            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@appointmentId", appointmentId);
            cmd.Parameters.AddWithValue("@symptoms", symptoms);
            cmd.Parameters.AddWithValue("@diagnosis", diagnosis);
            cmd.Parameters.AddWithValue("@notes", notes);

            int result = await cmd.ExecuteNonQueryAsync();
            return result > 0;
        }
    }
}