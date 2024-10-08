using ServiceMaintenance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceMaintenance.Api.Models
{
    public interface ICategoryServiceRespository
    {
        Task<IEnumerable<Category>> GetData();
        Task<Category> GetData(int ID);
        Task<Category> AddData(Category data);
        Task<Category> UpdateData(Category data);
        Task<Category> DeleteData(int ID);
    }
}
