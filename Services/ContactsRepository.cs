using AutoPartsApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoPartsApp.Services
{
    public class ContactsRepository : IRepository<Contact>
    {
        public Task<bool> CreateAsync(Contact item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                using (AutoPartsBaseEntities entities = new AutoPartsBaseEntities())
                {
                    return entities.Contacts.ToList();
                }
            });
        }

        public Task<Contact> GetSingleAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Contact item)
        {
            throw new NotImplementedException();
        }
    }
}
