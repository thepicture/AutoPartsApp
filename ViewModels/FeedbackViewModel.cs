using AutoPartsApp.Commands;
using AutoPartsApp.Models.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
            LoadUsersToWhoICanSendAsync()
                .ContinueWith(t =>
                {
                    LoadDialogueAsync();
                });
        }

        private async Task LoadUsersToWhoICanSendAsync()
        {
            IEnumerable<User> currentUsers = await UserRepository.GetAllAsync();
            currentUsers = currentUsers.Where(u =>
            {
                return u.Id != Identity.WeakTarget.Id;
            });
            UsersToWhoICanSend = new ObservableCollection<User>(currentUsers);
        }

        private async void LoadDialogueAsync()
        {
            if (CurrentInterlocutor == null)
            {
                return;
            }
            IEnumerable<Feedback> currentDialogue =
                await FeedbackRepository.GetAllAsync();
            currentDialogue = currentDialogue.Where(f =>
            {
                return (f.SenderUserId == CurrentInterlocutor.Id && f.ReceiverUserId == Identity.WeakTarget.Id)
                       || (f.SenderUserId == Identity.WeakTarget.Id && f.ReceiverUserId == CurrentInterlocutor.Id);
            });
            MyFeedbacks = new ObservableCollection<Feedback>(currentDialogue);
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
            CurrentFeedback.User1 = CurrentInterlocutor;
            if (await FeedbackRepository.CreateAsync(CurrentFeedback))
            {
                CurrentFeedback = new Feedback();
                LoadDialogueAsync();
            }
        }

        private Command openDialogCommand;
        private User currentInterlocutor;

        public ICommand OpenDialogCommand
        {
            get
            {
                if (openDialogCommand == null)
                    openDialogCommand = new Command(OpenDialog);

                return openDialogCommand;
            }
        }

        public User CurrentInterlocutor
        {
            get => currentInterlocutor;
            set
            {
                if (SetProperty(ref currentInterlocutor, value))
                {
                    LoadDialogueAsync();
                }
            }
        }

        private async void OpenDialog(object param)
        {
            User currentInterlocutor = param as User;
            using (AutoPartsBaseEntities entities = new AutoPartsBaseEntities())
            {
                entities.Users
                    .Find(currentInterlocutor.Id).Feedbacks
                    .ToList()
                    .ForEach(f => f.IsWatched = true);
                entities.SaveChanges();
            }
            await LoadUsersToWhoICanSendAsync();
            CurrentInterlocutor = currentInterlocutor;
        }
    }
}