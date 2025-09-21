using ConsoleAppCms2025.Repository;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
    public class LabResultServiceImpl : ILabResultService
    {
        private readonly ILabResultRepository _labRepo;

        public LabResultServiceImpl(ILabResultRepository labRepo)
        {
            _labRepo = labRepo;
        }

        public async Task AddLabResultAsync(int appointmentId, string labTestName, string labTestValue, string remarks)
        {
            await _labRepo.AddLabResultAsync(appointmentId, labTestName, labTestValue, remarks);
        }
    }
}
