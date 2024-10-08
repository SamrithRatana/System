using ServiceMaintenance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceMaintenance.Api.Models
{
    public interface IPrinterModelServiceRespository
    {
        Task<IEnumerable<PrinterModel>> GetData();
        Task<PrinterModel> GetData(int ID);
        Task<PrinterModel> AddData(PrinterModel data);
        Task<PrinterModel> UpdateData(PrinterModel data);
        Task<PrinterModel> DeleteData(int ID);
    }
}
