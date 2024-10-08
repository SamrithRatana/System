using ServiceMaintenance.Models;

namespace ServiceMaintenance.Services
{
    public interface IManufacturerService
    {
        Task<IEnumerable<Manufacturer>> GetData();
        Task<Manufacturer> GetData(int id);
        Task<Manufacturer> UpdateData(Manufacturer updatedData);
        Task<Manufacturer> CreateData(Manufacturer newData);
        Task DeleteData(int id);
    }
}
