using System.Threading.Tasks;

namespace ConsoleAppCms2025.Repository
{
    public interface ILabResultRepository
    {
        Task AddLabResultAsync(int appointmentId, string labTestName, string labTestValue, string remarks);
    }
}
