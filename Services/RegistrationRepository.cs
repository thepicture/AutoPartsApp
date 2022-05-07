using AutoPartsApp.Models.Entities;
using AutoPartsApp.Models.Specific;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoPartsApp.Services
{
    public class RegistrationRepository : IRepository<RegisterUser>
    {
        public async Task<bool> CreateAsync(RegisterUser item)
        {
            if (!item.IsValid)
            {
                await DependencyService
                    .Get<IFeedbackService>()
                    .InformErrorAsync("Исправьте ошибки полей " +
                    "перед сохранением пользователя");
                return false;
            }
            return await Task.Run(() =>
            {
                using (AutoPartsBaseEntities entities = new AutoPartsBaseEntities())
                {
                    User user = new User
                    {
                        Email = item.Email,
                        LastName = item.LastName,
                        FirstName = item.FirstName,
                        Patronymic = item.Patronymic,
                        UserRole = entities.UserRoles.Find(item.UserRole.Id),
                        PasswordHash = item.PasswordHash,
                        Salt = item.Salt,
                        Image = item.Image
                    };
                    entities.Users.Add(user);
                    try
                    {
                        entities.SaveChanges();
                        DependencyService
                            .Get<IFeedbackService>()
                            .InformAsync("Вы зарегистрированы");
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

        public Task<bool> DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RegisterUser>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RegisterUser> GetSingleAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(RegisterUser item)
        {
            throw new NotImplementedException();
        }
    }
}
