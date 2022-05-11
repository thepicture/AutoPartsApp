using AutoPartsApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoPartsApp.Services
{
    public class CategoriesRepository : IRepository<Category>
    {
        public Task<bool> CreateAsync(Category item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                using (AutoPartsBaseEntities entities = new AutoPartsBaseEntities())
                {
                    return entities.Categories.ToList();
                }
            });
        }

        public Task<Category> GetSingleAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Category item)
        {
            throw new NotImplementedException();
        }
    }
}
