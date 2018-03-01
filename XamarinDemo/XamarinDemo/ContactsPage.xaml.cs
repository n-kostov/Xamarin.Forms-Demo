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
                new Contact{Id = 1, DisplayName="Gosho", Name = "Georgi", PhoneNumber = "12345", DateOfBirth = new DateTime(1900, 5, 5) },
                new Contact{Id = 2, DisplayName="Pesho", Name = "Peter", Description ="I like beer!", PhoneNumber = "4234523", DateOfBirth = new DateTime(1985, 4, 1) },
                new Contact{Id = 3, DisplayName="Tosho", Name = "Todor", Description = "Hey!", PhoneNumber = "123125435", DateOfBirth = new DateTime(1888, 9, 3) }
            };

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
            await Navigation.PushAsync(new ContactDetailPage(contact));

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
            bool result = await DisplayAlert("Please confirm", "Do you want to add new item?", "Yes", "No");
            if (result)
            {
                string choice = await DisplayActionSheet("Please choose", "Cancel", "Delete", "Send Email", "Call");
                await DisplayAlert("Your choice is:", choice, "OK");
            }
        }
    }
}