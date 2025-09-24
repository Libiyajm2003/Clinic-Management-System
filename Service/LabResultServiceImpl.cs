using ConsoleAppCms2025.Repository;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
    /// <summary>
    /// Implements lab result operations by delegating to repository.
    /// </summary>
    public class LabResultServiceImpl : ILabResultService
    {
        private readonly ILabResultRepository _labRepo;

        /// <summary>
        /// Creates a new instance of <see cref="LabResultServiceImpl"/>.
        /// </summary>
        public LabResultServiceImpl(ILabResultRepository labRepo)
        {
            _labRepo = labRepo;
        }

        /// <inheritdoc />
        public async Task AddLabResultAsync(int appointmentId, string labTestName, string labTestValue, string remarks)
        {
            await _labRepo.AddLabResultAsync(appointmentId, labTestName, labTestValue, remarks);
        }
    }
}
