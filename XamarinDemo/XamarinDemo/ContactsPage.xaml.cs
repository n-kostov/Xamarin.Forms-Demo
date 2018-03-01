using System;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactsPage : ContentPage
    {
        private ObservableCollection<Contact> _contacts;
        public ObservableCollection<Contact> GetContacts(string searchQuery = null)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                return _contacts;
            }

            searchQuery = searchQuery.ToLowerInvariant();
            return new ObservableCollection<Contact>(
                _contacts.Where(c => c.DisplayName.ToLowerInvariant().StartsWith(searchQuery)));
        }

        public ContactsPage()
        {
            InitializeComponent();

            _contacts = new ObservableCollection<Contact>
            {
                new Contact{Id = 1, DisplayName="Gosho", Name = "Georgi", PhoneNumber = "12345", DateOfBirth = new DateTime(1900, 5, 5) },
                new Contact{Id = 2, DisplayName="Pesho", Name = "Peter", Description ="I like beer!", PhoneNumber = "4234523", DateOfBirth = new DateTime(1985, 4, 1), IsColleague = true },
                new Contact{Id = 3, DisplayName="Tosho", Name = "Todor", Description = "Hey!", PhoneNumber = "123125435", DateOfBirth = new DateTime(1888, 9, 3) }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            contactsListView.ItemsSource = GetContacts();
        }

        private async void contactsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            var contact = e.SelectedItem as Contact;
            ContactDetailPage detailPage = new ContactDetailPage(contact);
            detailPage.ContactAdded += (s, contactArgs) =>
            {
                if (contactArgs.Contact.Id != 0)
                {
                    contact.DisplayName = contactArgs.Contact.DisplayName;
                    contact.Name = contactArgs.Contact.Name;
                    contact.Description = contactArgs.Contact.Description;
                    contact.PhoneNumber = contactArgs.Contact.PhoneNumber;
                    contact.DateOfBirth = contactArgs.Contact.DateOfBirth;
                    contact.IsColleague = contactArgs.Contact.IsColleague;
                }
            };
            await Navigation.PushAsync(detailPage);

            contactsListView.SelectedItem = null;
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var contact = menuItem.CommandParameter as Contact;

            _contacts.Remove(contact);
        }

        private void contactsListView_Refreshing(object sender, EventArgs e)
        {
            contactsListView.ItemsSource = GetContacts();

            contactsListView.EndRefresh();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            contactsListView.ItemsSource = GetContacts(e.NewTextValue);
        }

        private async void PlusToolbarItem_Activated(object sender, EventArgs e)
        {
            var contact = new Contact();
            ContactDetailPage detailPage = new ContactDetailPage(contact);
            detailPage.ContactAdded += (s, contactArgs) =>
            {
                if (contactArgs.Contact.Id == 0)
                {
                    contactArgs.Contact.Id = _contacts.Max(x => x.Id) + 1;
                    _contacts.Add(contactArgs.Contact);
                }
            };

            await Navigation.PushAsync(detailPage);
        }
    }
}