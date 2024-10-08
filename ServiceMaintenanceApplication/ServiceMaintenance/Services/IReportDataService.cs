using ServiceMaintenance.Models;

namespace ServiceMaintenance.Services
{
    public interface IReportDataService
    {
        Task<IEnumerable<ServiceReportData>> GetReport();
        Task<ServiceReportData> GetReport(int id);
        Task<ServiceReportData> UpdateReport(ServiceReportData updatedReport);
        Task<ServiceReportData> CreateReport(ServiceReportData newReport);
        Task DeleteReport(int id);
        Task<byte[]> GenerateReportAsync(int id);
    }
}
