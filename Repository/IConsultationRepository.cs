using ConsoleAppCms2025.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Repository
{
    public interface IConsultationRepository
    {
        Task<bool> AddConsultationAsync(int appointmentId, string symptoms, string diagnosis, string notes);
    }
}
 
