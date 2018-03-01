using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinDemo.Services;

namespace XamarinDemo.ViewModels
{
    public class ContactsPageViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;
        private readonly IPageService _pageService;
        private readonly ContactService _contactService;
        private ContactViewModel _selectedContact;
        private string _searchQuery;
        private bool _isRefreshing = false;
        public ObservableCollection<ContactViewModel> Contacts { get; set; }
        public ContactViewModel SelectedContact
        {
            get { return _selectedContact; }
            set { SetValue(ref _selectedContact, value); }
        }

        public string SearchQuery
        {
            get { return _searchQuery; }
            set { SetValue(ref _searchQuery, value); }
        }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetValue(ref _isRefreshing, value); }
        }

        public ICommand AddCommand { get; set; }
        public ICommand SelectCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        public ContactsPageViewModel(INavigation navigation, IPageService pageService, string accessToken)
        {
            _navigation = navigation;
            _pageService = pageService;

            _contactService = new ContactService(accessToken);

            Contacts = new ObservableCollection<ContactViewModel>();

            AddCommand = new Command(async () => await AddContact());
            SelectCommand = new Command(async () => await SelectContact());
            DeleteCommand = new Command<ContactViewModel>(async (contact) => await DeleteContact(contact));
            SearchCommand = new Command(async () => await LoadContacts());
            RefreshCommand = new Command(async () => await RefreshContacts());
        }

        public async Task LoadContacts()
        {
            var contactsDb = await _contactService.GetContacts(SearchQuery);
            Contacts.Clear();

            foreach (var c in contactsDb.Select(x => new ContactViewModel
                {
                    DateOfBirth = x.DateOfBirth,
                    Description = x.Description,
                    DisplayName = x.DisplayName,
                    Id = x.Id,
                    IsColleague = x.IsColleague,
                    Name = x.Name,
                    PhoneNumber = x.PhoneNumber
                }
                ))
            {
                Contacts.Add(c);
            }
        }

        private async Task AddContact()
        {
            await _navigation.PushAsync(new ContactDetailPage(new ContactViewModel()));
        }

        private async Task SelectContact()
        {
            if (SelectedContact == null)
            {
                return;
            }

            await _navigation.PushAsync(new ContactDetailPage(SelectedContact));

            SelectedContact = null;
        }

        private async Task DeleteContact(ContactViewModel contact)
        {
            bool response = await _pageService.DisplayAlert("Delete", "Do you want to delete this contact?", "Yes", "No");
            if (response)
            {
                await _contactService.DeleteContact(new Contact
                {
                    Id = contact.Id
                });

                Contacts.Remove(contact);
            }
        }

        private async Task RefreshContacts()
        {
            IsRefreshing = true;

            await LoadContacts();

            IsRefreshing = false;
        }
    }
}