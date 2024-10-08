using Microsoft.EntityFrameworkCore;
using ServiceMaintenance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceMaintenance.Api.Models
{
    public class PrinterModelServiceRespository : IPrinterModelServiceRespository
    {
        private readonly ServiceMantenanceContext _appDbContext;

        public PrinterModelServiceRespository(ServiceMantenanceContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<PrinterModel> AddData(PrinterModel data)
        {
            var result = await _appDbContext.PrinterModels.AddAsync(data);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<PrinterModel> DeleteData(int ID)
        {
            var result = await _appDbContext.PrinterModels
                          .FirstOrDefaultAsync(e => e.PrinterModelID == ID);
            if (result != null)
            {
                _appDbContext.PrinterModels.Remove(result);
                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<PrinterModel>> GetData()
        {
            return await _appDbContext.PrinterModels.ToListAsync();
        }

        public async Task<PrinterModel> GetData(int ID)
        {
            return await _appDbContext.PrinterModels
                        .FirstOrDefaultAsync(e => e.PrinterModelID == ID);
        }

        public async Task<PrinterModel> UpdateData(PrinterModel data)
        {
            var printerModel = await _appDbContext.PrinterModels
                .FirstOrDefaultAsync(e => e.PrinterModelID == data.PrinterModelID);

            if (printerModel != null)
            {
                printerModel.ModelName = data.ModelName;
                printerModel.Photo = data.Photo;
                printerModel.Description = data.Description;

                await _appDbContext.SaveChangesAsync();

                return printerModel;
            }

            return null;
        }
    }
}
