using ServiceMaintenance.Models;

namespace ServiceMaintenance.Api.Models
{
    public interface IManufacturerRespository
    {
        Task<IEnumerable<Manufacturer>> GetData();
        Task<Manufacturer> GetData(int ID);
        Task<Manufacturer> AddData(Manufacturer data);
        Task<Manufacturer> UpdateData(Manufacturer data);
        Task<Manufacturer> DeleteData(int ID);
    }
}
