using ServiceMaintenance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceMaintenance.Api.Models
{
    public interface ISparePartServiceRespository
    {
        Task<IEnumerable<SparePart>> GetData();
        Task<SparePart> GetData(int ID);
        Task<SparePart> AddData(SparePart data);
        Task<SparePart> UpdateData(SparePart data);
        Task<SparePart> DeleteData(int ID);
    }
}
