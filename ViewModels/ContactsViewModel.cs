using AutoPartsApp.Models.Entities;
using System;
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
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                currentContacts = currentContacts.Where(c =>
                {
                    return c.Address.IndexOf(SearchText,
                                             StringComparison.OrdinalIgnoreCase) != -1
                           || SearchText.FromPhoneNumberToDigits().Count() > 0 && c.PhoneNumber
                                .FromPhoneNumberToDigits()
                                .Contains(SearchText.FromPhoneNumberToDigits());
                });
            }
            Contacts = new ObservableCollection<Contact>(currentContacts);
        }

        private string searchText;

        public string SearchText
        {
            get => searchText;
            set
            {
                if (SetProperty(ref searchText, value))
                {
                    LoadContactsAsync();
                }
            }
        }
    }
}