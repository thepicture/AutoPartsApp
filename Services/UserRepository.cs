using AutoPartsApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AutoPartsApp.Services
{
    public class UserRepository : IRepository<User>
    {
        public Task<bool> CreateAsync(User item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                using (AutoPartsBaseEntities entities = new AutoPartsBaseEntities())
                {
                    return entities.Users
                    .Include(u => u.UserRole)
                    .Include(u => u.Feedbacks)
                    .Include(u => u.Feedbacks1)
                    .ToList();
                }
            });
        }

        public Task<User> GetSingleAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(User item)
        {
            if (!item.IsValid)
            {
                await DependencyService
                    .Get<IFeedbackService>()
                    .InformErrorAsync("Исправьте ошибки полей " +
                    "перед сохранением");
                return false;
            }
            return await Task.Run(() =>
            {
                using (AutoPartsBaseEntities entities = new AutoPartsBaseEntities())
                {
                    User currentUser = entities.Users.Find(item.Id);
                    entities
                        .Entry(currentUser).CurrentValues
                        .SetValues(item);
                    try
                    {
                        entities.SaveChanges();
                        DependencyService
                            .Get<IFeedbackService>()
                            .InformAsync("Личный кабинет обновлён");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        DependencyService
                            .Get<IFeedbackService>()
                            .InformErrorAsync(ex);
                        return false;
                    }
                }
            });
        }
    }
}
