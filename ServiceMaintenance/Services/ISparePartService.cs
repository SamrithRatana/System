using ServiceMaintenance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceMaintenance.Services
{
    public interface ISparePartService
    {
        Task<IEnumerable<SparePart>> GetData();
        Task<SparePart> GetData(int id);
        Task<SparePart> UpdateData(SparePart updatedData);
        Task<SparePart> CreateData(SparePart newData);
        Task DeleteData(int id);
    }
}
