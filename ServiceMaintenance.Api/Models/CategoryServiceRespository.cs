using Microsoft.EntityFrameworkCore;
using ServiceMaintenance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceMaintenance.Api.Models
{
    public class CategoryServiceRespository : ICategoryServiceRespository
    {
        private readonly ServiceMantenanceContext _appDbContext;

        public CategoryServiceRespository(ServiceMantenanceContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Category> AddData(Category data)
        {
            var result = await _appDbContext.Categories.AddAsync(data);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Category> DeleteData(int ID)
        {
            var result = await _appDbContext.Categories
                          .FirstOrDefaultAsync(e => e.CategoryID == ID);
            if (result != null)
            {
                _appDbContext.Categories.Remove(result);
                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Category>> GetData()
        {
            return await _appDbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetData(int ID)
        {
            return await _appDbContext.Categories
                        .FirstOrDefaultAsync(e => e.CategoryID == ID);
        }

        public async Task<Category> UpdateData(Category data)
        {
            var category = await _appDbContext.Categories
                .FirstOrDefaultAsync(e => e.CategoryID == data.CategoryID);

            if (category != null)
            {
                category.CategoryName = data.CategoryName;
                category.Description = data.Description;

                await _appDbContext.SaveChangesAsync();

                return category;
            }

            return null;
        }
    }
}
