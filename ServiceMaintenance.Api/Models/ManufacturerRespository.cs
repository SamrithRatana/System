using Microsoft.EntityFrameworkCore;
using ServiceMaintenance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceMaintenance.Api.Models
{
    public class ManufacturerRespository : IManufacturerRespository
    {
        private readonly ServiceMantenanceContext _appDbContext;

        public ManufacturerRespository(ServiceMantenanceContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Manufacturer>> GetData()
        {
            return await _appDbContext.Manufacturers.ToListAsync();
        }

        public async Task<Manufacturer> GetData(int ID)
        {
            return await _appDbContext.Manufacturers
                       .FirstOrDefaultAsync(e => e.ManufacturerID == ID);
        }

        public async Task<Manufacturer> AddData(Manufacturer data)
        {
            var result = await _appDbContext.Manufacturers.AddAsync(data);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Manufacturer> UpdateData(Manufacturer data)
        {
            var manufacturer = await _appDbContext.Manufacturers
                                 .FirstOrDefaultAsync(e => e.ManufacturerID == data.ManufacturerID);

            if (manufacturer != null)
            {
                manufacturer.Type = data.Type;
                manufacturer.Name = data.Name;
                manufacturer.Logo = data.Logo;
                manufacturer.Description = data.Description;

                await _appDbContext.SaveChangesAsync();
                return manufacturer;
            }

            return null;
        }

        public async Task<Manufacturer> DeleteData(int ID)
        {
            var manufacturer = await _appDbContext.Manufacturers
                                 .FirstOrDefaultAsync(e => e.ManufacturerID == ID);

            if (manufacturer != null)
            {
                _appDbContext.Manufacturers.Remove(manufacturer);
                await _appDbContext.SaveChangesAsync();
                return manufacturer;
            }

            return null;
        }
    }
}
