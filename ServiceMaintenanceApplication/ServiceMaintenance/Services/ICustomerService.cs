using ServiceMaintenance.Models;

namespace ServiceMaintenance.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetReport();
        Task<Customer> GetReport(int id);
        Task<Customer> UpdateReport(Customer updatedReport);
        Task<Customer> CreateReport(Customer newReport);
        Task DeleteReport(int id);
    }
}
