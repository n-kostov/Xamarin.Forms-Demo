using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinDemo.Services;

namespace XamarinDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FindNewContactPage : ContentPage
    {
        private static readonly App CurrentApp = Application.Current as App;
        private readonly ContactService _contactService = new ContactService(CurrentApp.AccessToken);

        public FindNewContactPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            contactSearchBar.Text = null;
            contactsListView.ItemsSource = new List<Contact>();

            base.OnAppearing();
        }

        private async void contactsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            var contact = e.SelectedItem as Contact;
            ContactDetailPage detailPage = new ContactDetailPage(new ViewModels.ContactViewModel());

            await Navigation.PushAsync(detailPage);

            contactsListView.SelectedItem = null;
        }

        private async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            if (contactSearchBar.Text.Length > 3)
            {
                searchingIndricator.IsRunning = true;
                contactsListView.ItemsSource = await _contactService.FindNewContacts(contactSearchBar.Text);
                searchingIndricator.IsRunning = false;
            }
        }
    }
}