using AutoPartsApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AutoPartsApp.Services
{
    public class PartRepository : IRepository<Part>
    {
        public Task<bool> CreateAsync(Part item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Part>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                using (AutoPartsBaseEntities entities = new AutoPartsBaseEntities())
                {
                    return entities.Parts
                    .Include(p => p.Provider)
                    .Include(p => p.Manufacturer)
                    .ToList();
                }
            });
        }

        public Task<Part> GetSingleAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Part item)
        {
            throw new NotImplementedException();
        }
    }
}
