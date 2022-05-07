using AutoPartsApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoPartsApp.Services
{
    public class UserRoleRepository : IRepository<UserRole>
    {
        public Task<bool> CreateAsync(UserRole item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                using (AutoPartsBaseEntities entities = new AutoPartsBaseEntities())
                {
                    return entities.UserRoles.ToList();
                }
            });
        }

        public Task<UserRole> GetSingleAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(UserRole item)
        {
            throw new NotImplementedException();
        }
    }
}
