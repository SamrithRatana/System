using ServiceMaintenance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceMaintenance.Services
{
    public interface IPrinterModelService
    {
        Task<IEnumerable<PrinterModel>> GetData();
        Task<PrinterModel> GetData(int id);
        Task<PrinterModel> UpdateData(PrinterModel updatedData);
        Task<PrinterModel> CreateData(PrinterModel newData);
        Task DeleteData(int id);
    }
}
