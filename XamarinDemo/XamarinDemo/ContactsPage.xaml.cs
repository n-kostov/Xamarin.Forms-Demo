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
            _contacts = new ObservableCollection<Contact>
            {
                new Contact{Id = 1, Name="Gosho" },
                new Contact{Id = 2, Name="Pesho", Description ="I like beer!" },
                new Contact{Id = 3, Name="Tosho", Description = "Hey!" }
            };

            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                return _contacts;
            }

            searchQuery = searchQuery.ToLowerInvariant();
            return new ObservableCollection<Contact>(
                _contacts.Where(c => c.Name.ToLowerInvariant().StartsWith(searchQuery)));
        }

        public ContactsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            contactsListView.ItemsSource = GetContacts();
        }

        private void contactsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            var contact = e.SelectedItem as Contact;
            DisplayAlert("Select", contact.Name, "OK");

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
    }
}