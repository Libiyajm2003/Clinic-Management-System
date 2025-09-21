using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
    public interface IConsultationService
    {
        Task<bool> AddConsultationAsync(int appointmentId, string symptoms, string diagnosis, string notes);
    }
}