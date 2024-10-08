using ServiceMaintenance.Models;

namespace ServiceMaintenance.Api.Models
{
    public interface ICustomerServiceRespository
    {
        Task<IEnumerable<Customer>> GetReport();
        Task<Customer> GetReport(int ID);
        Task<Customer> AddReport(Customer report);
        Task<Customer> UpdateReport(Customer report);
        Task<Customer> DeleteReport(int ID);
    }
}
