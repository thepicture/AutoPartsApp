using System.Threading.Tasks;

namespace AutoPartsApp.Services
{
    public interface IFeedbackService
    {
        Task InformAsync(object information);
        Task WarnAsync(object warning);
        Task<bool> AskAsync(object question);
        Task InformErrorAsync(object error);
    }
}
