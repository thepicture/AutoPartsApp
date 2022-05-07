using AutoPartsApp.Models.Entities;
using AutoPartsApp.Models.Specific;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoPartsApp.Services
{
    public class LoginRepository : IRepository<LoginUser>
    {
        public async Task<bool> CreateAsync(LoginUser item)
        {
            return await Task.Run(() =>
            {
                using (AutoPartsBaseEntities entities = new AutoPartsBaseEntities())
                {
                    if (!string.IsNullOrWhiteSpace(item.Email)
                        && !string.IsNullOrWhiteSpace(item.Password)
                        && entities.Users
                            .AsNoTracking()
                            .FirstOrDefault(u => u.Email == item.Email)
                                is User currentUser)
                    {
                        DependencyService
                        .Get<IHashGenerator>()
                        .GenerateHash(item.Password,
                                      out byte[] hash,
                                      out byte[] salt,
                                      inputSalt: currentUser.Salt);
                        if (Enumerable.SequenceEqual(currentUser.PasswordHash, hash))
                        {
                            entities.SaveChanges();
                            DependencyService
                                .Get<IFeedbackService>()
                                .InformAsync($"Вы вошли в аккаунт с ролью {currentUser.UserRole.Title}");
                            return true;
                        }
                    }
                }
                DependencyService
                    .Get<IFeedbackService>()
                    .InformAsync($"Неверная электронная почта или пароль");
                return false;
            });
        }

        public Task<bool> DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LoginUser>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<LoginUser> GetSingleAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(LoginUser item)
        {
            throw new NotImplementedException();
        }
    }
}
