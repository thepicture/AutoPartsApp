using AutoPartsApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AutoPartsApp.Services
{
    public class FeedbackRepository : IRepository<Feedback>
    {
        public async Task<bool> CreateAsync(Feedback item)
        {
            if (!item.IsValid)
            {
                await DependencyService
                    .Get<IFeedbackService>()
                    .InformErrorAsync("Исправьте ошибки полей " +
                    "перед сохранением обратной связи");
                return false;
            }
            item.PostingDateTime = DateTime.Now;
            item.SenderUserId = DependencyService
                .Get<IIdentity<User>>().WeakTarget.Id;
            item.ReceiverUserId = item.User1.Id;
            item.User1 = null;
            return await Task.Run(() =>
            {
                using (AutoPartsBaseEntities entities = new AutoPartsBaseEntities())
                {
                    entities.Feedbacks.Add(item);
                    try
                    {
                        entities.SaveChanges();
                        DependencyService
                            .Get<IFeedbackService>()
                            .InformAsync("Обратная связь отправлена");
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

        public async Task<IEnumerable<Feedback>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                using (AutoPartsBaseEntities entities = new AutoPartsBaseEntities())
                {
                    return entities.Feedbacks
                    .Include(f => f.User)
                    .Include(f => f.User1)
                    .ToList();
                }
            });
        }

        public Task<Feedback> GetSingleAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Feedback item)
        {
            throw new NotImplementedException();
        }
    }
}
