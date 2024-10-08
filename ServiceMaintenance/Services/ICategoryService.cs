using ServiceMaintenance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceMaintenance.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetData();
        Task<Category> GetData(int id);
        Task<Category> UpdateData(Category updatedData);
        Task<Category> CreateData(Category newData);
        Task DeleteData(int id);
    }
}
