using ConsoleAppCms2025.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Repository
{
    /// <summary>
    /// SQL Server implementation of billing data access operations.
    /// </summary>
    public class BillingRepositoryImpl : IBillingRepository
    {
        private readonly string _connectionString;

        public BillingRepositoryImpl()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CsWinSql"].ConnectionString;
        }

        /// <inheritdoc />
        public async Task<int> AddBillingAsync(Billing bill)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            await con.OpenAsync();

            string query = @"INSERT INTO TblBilling 
                             (AppointmentId, PatientMMR, DoctorId, ConsultationFee, MedicineCharges, LabCharges, TotalAmount, BillDate, IsPaid)
                             VALUES (@AppointmentId, @PatientMMR, @DoctorId, @ConsultationFee, @MedicineCharges, @LabCharges, @TotalAmount, @BillDate, @IsPaid)";

            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@AppointmentId", bill.AppointmentId);
            cmd.Parameters.AddWithValue("@PatientMMR", bill.PatientMMR);
            cmd.Parameters.AddWithValue("@DoctorId", bill.DoctorId);
            cmd.Parameters.AddWithValue("@ConsultationFee", bill.ConsultationFee);
            cmd.Parameters.AddWithValue("@MedicineCharges", bill.MedicineCharges);
            cmd.Parameters.AddWithValue("@LabCharges", bill.LabCharges);
            cmd.Parameters.AddWithValue("@TotalAmount", bill.TotalAmount);
            cmd.Parameters.AddWithValue("@BillDate", bill.BillDate);
            cmd.Parameters.AddWithValue("@IsPaid", bill.IsPaid);

            return await cmd.ExecuteNonQueryAsync();
        }

        /// <inheritdoc />
        public async Task<Billing> GetBillingByAppointmentIdAsync(int appointmentId)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            await con.OpenAsync();

            string query = "SELECT * FROM TblBilling WHERE AppointmentId=@AppointmentId";
            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Billing
                {
                    BillId = Convert.ToInt32(reader["BillId"]),
                    AppointmentId = Convert.ToInt32(reader["AppointmentId"]),
                    PatientMMR = reader["PatientMMR"].ToString(),
                    DoctorId = Convert.ToInt32(reader["DoctorId"]),
                    ConsultationFee = reader["ConsultationFee"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["ConsultationFee"]),
                    MedicineCharges = reader["MedicineCharges"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["MedicineCharges"]),
                    LabCharges = reader["LabCharges"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["LabCharges"]),
                    TotalAmount = reader["TotalAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalAmount"]),
                    BillDate = reader["BillDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["BillDate"]),
                    IsPaid = reader["IsPaid"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsPaid"])
                };
            }

            return null;
        }

        /// <inheritdoc />
        public async Task<List<Billing>> GetBillingsByPatientMMRAsync(string patientMMR)
        {
            var billings = new List<Billing>();

            using SqlConnection con = new SqlConnection(_connectionString);
            await con.OpenAsync();

            string query = @"SELECT BillId, AppointmentId, DoctorId, ConsultationFee, MedicineCharges, LabCharges, TotalAmount, BillDate, IsPaid
                             FROM TblBilling
                             WHERE PatientMMR = @mmr";

            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@mmr", patientMMR);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                billings.Add(new Billing
                {
                    BillId = Convert.ToInt32(reader["BillId"]),
                    AppointmentId = Convert.ToInt32(reader["AppointmentId"]),
                    DoctorId = reader["DoctorId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["DoctorId"]),
                    ConsultationFee = reader["ConsultationFee"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["ConsultationFee"]),
                    MedicineCharges = reader["MedicineCharges"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["MedicineCharges"]),
                    LabCharges = reader["LabCharges"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["LabCharges"]),
                    TotalAmount = reader["TotalAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalAmount"]),
                    BillDate = reader["BillDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["BillDate"]),
                    IsPaid = reader["IsPaid"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsPaid"])
                });
            }

            return billings;
        }

        /// <inheritdoc />
        public async Task<decimal> GetMedicineChargesByAppointmentAsync(int appointmentId)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            await con.OpenAsync();

            string query = @"
                SELECT SUM(M.UnitPrice * MP.Qty)
                FROM TblMedicinePrescription MP
                JOIN TblMedicine M ON MP.MedicineId = M.MedicineId
                WHERE MP.AppointmentId = @AppointmentId";

            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);

            object result = await cmd.ExecuteScalarAsync();
            return result == DBNull.Value || result == null ? 0 : Convert.ToDecimal(result);
        }

        /// <inheritdoc />
        public async Task<decimal> GetLabChargesByAppointmentAsync(int appointmentId)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            await con.OpenAsync();

            string query = @"
                SELECT SUM(LT.Amount)
                FROM TblLabPrescription LP
                JOIN TblLabTest LT ON LP.LabTestId = LT.LabTestId
                WHERE LP.AppointmentId = @AppointmentId";

            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);

            object result = await cmd.ExecuteScalarAsync();
            return result == DBNull.Value || result == null ? 0 : Convert.ToDecimal(result);
        }
    }
}
