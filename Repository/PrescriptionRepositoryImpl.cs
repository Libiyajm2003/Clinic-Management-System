using ConsoleAppCms2025.Model;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Repository
{
    public class PrescriptionRepositoryImpl : IPrescriptionRepository
    {
        private readonly string connectionString = @"Data Source=.;Initial Catalog=cmsv2025db;Integrated Security=True;TrustServerCertificate=True";

        public async Task AddPrescriptionAsync(MedicinePrescription prescription)
        {
            using SqlConnection con = new SqlConnection(connectionString);

            string query = @"
INSERT INTO TblMedicinePrescription (MedicineId, Dosage, Duration, Qty, AppointmentId)
SELECT MedicineId, @Dosage, @Duration, @Qty, @AppointmentId
FROM TblMedicine
WHERE MedicineName = @MedicineName
";

            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@AppointmentId", prescription.AppointmentId);
            cmd.Parameters.AddWithValue("@MedicineName", prescription.MedicineName); // fetches MedicineId automatically
            cmd.Parameters.AddWithValue("@Dosage", prescription.Dosage);
            cmd.Parameters.AddWithValue("@Duration", prescription.Duration);
            cmd.Parameters.AddWithValue("@Qty", prescription.Qty);

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
