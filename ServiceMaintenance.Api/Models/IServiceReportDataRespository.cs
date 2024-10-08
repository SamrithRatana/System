using ServiceMaintenance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceMaintenance.Api.Models
{
    public interface IServiceReportDataRespository
    {
        Task<IEnumerable<ServiceReportData>> GetReport();
        Task<ServiceReportData> GetReport(int ID);
        // Task<Employee> ValidateEmployeeByEmail(string email);
        //  Task<IEnumerable<Employee>> Search(string name, Gender? gender);

        Task<ServiceReportData> AddReport(ServiceReportData report);
        Task<ServiceReportData> UpdateReport(ServiceReportData report);
        Task<ServiceReportData> DeleteReport(int ID);

    }
}
