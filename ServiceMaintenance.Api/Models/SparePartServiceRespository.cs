using Microsoft.EntityFrameworkCore;
using ServiceMaintenance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceMaintenance.Api.Models
{
    public class SparePartServiceRespository : ISparePartServiceRespository
    {
        private readonly ServiceMantenanceContext _appDbContext;

        public SparePartServiceRespository(ServiceMantenanceContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<SparePart> AddData(SparePart data)
        {
            var result = await _appDbContext.SpareParts.AddAsync(data);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<SparePart> DeleteData(int ID)
        {
            var result = await _appDbContext.SpareParts
                          .FirstOrDefaultAsync(e => e.SparePartID == ID);
            if (result != null)
            {
                _appDbContext.SpareParts.Remove(result);
                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<SparePart>> GetData()
        {
            return await _appDbContext.SpareParts.ToListAsync();
        }

        public async Task<SparePart> GetData(int ID)
        {
            return await _appDbContext.SpareParts
                        .FirstOrDefaultAsync(e => e.SparePartID == ID);
        }

        public async Task<SparePart> UpdateData(SparePart data)
        {
            var sparePart = await _appDbContext.SpareParts
                .FirstOrDefaultAsync(e => e.SparePartID == data.SparePartID);

            if (sparePart != null)
            {
                sparePart.PartName = data.PartName;
                sparePart.Description = data.Description;
                sparePart.PartNumber = data.PartNumber;
                sparePart.Qty = data.Qty;             
                sparePart.Photo = data.Photo;

                await _appDbContext.SaveChangesAsync();

                return sparePart;
            }

            return null;
        }
    }
}
