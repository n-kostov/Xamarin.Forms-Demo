using System;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinDemo.Services;

namespace XamarinDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactsPage : ContentPage
    {
        private static readonly App CurrentApp = Application.Current as App;
        private readonly ContactService _contactService = new ContactService(CurrentApp.AccessToken);

        public ContactsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            contactsListView.ItemsSource = await _contactService.GetContacts(searchBar.Text);
        }

        private async void contactsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            var contact = e.SelectedItem as Contact;
            ContactDetailPage detailPage = new ContactDetailPage(contact);
            detailPage.ContactAdded += async (s, contactArgs) =>
            {
                if (contactArgs.Contact.Id != 0)
                {
                    contact.DisplayName = contactArgs.Contact.DisplayName;
                    contact.Name = contactArgs.Contact.Name;
                    contact.Description = contactArgs.Contact.Description;
                    contact.PhoneNumber = contactArgs.Contact.PhoneNumber;
                    contact.DateOfBirth = contactArgs.Contact.DateOfBirth;
                    contact.IsColleague = contactArgs.Contact.IsColleague;

                    await _contactService.SaveContact(contact);
                }
            };
            await Navigation.PushAsync(detailPage);

            contactsListView.SelectedItem = null;
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var contact = menuItem.CommandParameter as Contact;

            (contactsListView.ItemsSource as ObservableCollection<Contact>).Remove(contact);
            await _contactService.DeleteContact(contact);
        }

        private async void contactsListView_Refreshing(object sender, EventArgs e)
        {
            contactsListView.ItemsSource = await _contactService.GetContacts();

            contactsListView.EndRefresh();
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            contactsListView.ItemsSource = await _contactService.GetContacts(e.NewTextValue);
        }

        private async void PlusToolbarItem_Activated(object sender, EventArgs e)
        {
            var contact = new Contact();
            ContactDetailPage detailPage = new ContactDetailPage(contact);
            detailPage.ContactAdded += async (s, contactArgs) =>
            {
                if (contactArgs.Contact.Id == 0)
                {
                    Contact savedContact = await _contactService.SaveContact(contactArgs.Contact);
                }
            };

            await Navigation.PushAsync(detailPage);
        }
    }
}