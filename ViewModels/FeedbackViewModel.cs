using AutoPartsApp.Commands;
using AutoPartsApp.Models.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AutoPartsApp.ViewModels
{
    public class FeedbackViewModel : BaseViewModel
    {
        public Feedback CurrentFeedback { get; set; } = new Feedback();
        public FeedbackViewModel()
        {
            Title = "Обратная связь";
            MyFeedbacks = new ObservableCollection<Feedback>();
            UsersToWhoICanSend = new ObservableCollection<User>();
            LoadMyFeedbacksAsync();
            LoadUsersToWhoICanSendAsync();
        }

        private async void LoadUsersToWhoICanSendAsync()
        {
            IEnumerable<User> currentUsers = await UserRepository.GetAllAsync();
            currentUsers = currentUsers.Where(u =>
            {
                return u.Id != Identity.WeakTarget.Id;
            });
            UsersToWhoICanSend = new ObservableCollection<User>(currentUsers);
        }

        private async void LoadMyFeedbacksAsync()
        {
            IEnumerable<Feedback> currentFeedbacks =
                await FeedbackRepository.GetAllAsync();
            currentFeedbacks = currentFeedbacks.Where(f =>
            {
                return f.ReceiverUserId == Identity.WeakTarget.Id || f.SenderUserId == Identity.WeakTarget.Id;
            });
            MyFeedbacks = new ObservableCollection<Feedback>(currentFeedbacks);
        }

        public ObservableCollection<User> UsersToWhoICanSend { get; set; }
        public ObservableCollection<Feedback> MyFeedbacks { get; set; }

        private Command sendFeedbackCommand;

        public ICommand SendFeedbackCommand
        {
            get
            {
                if (sendFeedbackCommand == null)
                    sendFeedbackCommand = new Command(SendFeedbackAsync);

                return sendFeedbackCommand;
            }
        }

        private async void SendFeedbackAsync()
        {
            if (await FeedbackRepository.CreateAsync(CurrentFeedback))
            {
                CurrentFeedback = new Feedback();
                LoadMyFeedbacksAsync();
            }
        }
    }
}