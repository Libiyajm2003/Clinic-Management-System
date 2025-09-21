using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
    public interface ILabResultService
    {
        Task AddLabResultAsync(int appointmentId, string labTestName, string labTestValue, string remarks);
    }
}
