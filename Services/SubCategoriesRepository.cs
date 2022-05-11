using AutoPartsApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoPartsApp.Services
{
    public class SubCategoriesRepository : IRepository<SubCategory>
    {
        public Task<bool> CreateAsync(SubCategory item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SubCategory>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                using (AutoPartsBaseEntities entities = new AutoPartsBaseEntities())
                {
                    return entities.SubCategories.ToList();
                }
            });
        }

        public Task<SubCategory> GetSingleAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(SubCategory item)
        {
            throw new NotImplementedException();
        }
    }
}
