using AutoPartsApp.Models.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AutoPartsApp.ViewModels
{
    public class ContactsViewModel : BaseViewModel
    {
        public ContactsViewModel()
        {
            Title = "Контакты";
            Contacts = new ObservableCollection<Contact>();
            LoadContactsAsync();
        }

        public ObservableCollection<Contact> Contacts { get; set; }

        private async void LoadContactsAsync()
        {
            IEnumerable<Contact> currentContacts =
                await ContactsRepository.GetAllAsync();
            if (!string.IsNullOrWhiteSpace(PhoneNumberSearchText))
            {
                currentContacts = currentContacts.Where(c =>
                {
                    return c.PhoneNumber
                        .FromPhoneNumberToDigits()
                        .Contains(
                            PhoneNumberSearchText.FromPhoneNumberToDigits());
                });
            }
            Contacts = new ObservableCollection<Contact>(currentContacts);
        }

        private string phoneNumberSearchText;

        public string PhoneNumberSearchText
        {
            get => phoneNumberSearchText;
            set
            {
                if (SetProperty(ref phoneNumberSearchText, value))
                {
                    LoadContactsAsync();
                }
            }
        }
    }
}