using ConsoleAppCms2025.Model;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Repository
{
    /// <summary>
    /// SQL Server implementation of lab result persistence.
    /// </summary>
    public class LabResultRepositoryImpl : ILabResultRepository
    {
        private readonly string connectionString = @"Data Source=.;Initial Catalog=cmsv2025db;Integrated Security=True;TrustServerCertificate=True";

        private SqlConnection GetConnection()
        {
            var builder = new SqlConnectionStringBuilder(connectionString)
            {
                Encrypt = false
            };
            return new SqlConnection(builder.ConnectionString);
        }

        /// <inheritdoc />
        public async Task AddLabResultAsync(int appointmentId, string labTestName, string labTestValue, string remarks)
        {
            using SqlConnection con = GetConnection();
            await con.OpenAsync();

            string query = @"
                INSERT INTO TblLabPrescription (LabTestId, AppointmentId, LabTestValue, Remarks, CreatedDate)
                SELECT LabTestId, @AppointmentId, @LabTestValue, @Remarks, GETDATE()
                FROM TblLabTest
                WHERE TestName = @LabTestName
            ";

            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);
            cmd.Parameters.AddWithValue("@LabTestName", labTestName);
            cmd.Parameters.AddWithValue("@LabTestValue", labTestValue);
            cmd.Parameters.AddWithValue("@Remarks", remarks);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}
