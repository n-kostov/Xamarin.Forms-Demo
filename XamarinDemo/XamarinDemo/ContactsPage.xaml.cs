using System;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinDemo.Services;
using XamarinDemo.ViewModels;

namespace XamarinDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactsPage : ContentPage
    {
        public ContactsPageViewModel ViewModel
        {
            get { return BindingContext as ContactsPageViewModel; }
            set { BindingContext = value; }
        }

        public ContactsPage()
        {
            InitializeComponent();

            ViewModel = new ContactsPageViewModel(Navigation, new PageService(), (Application.Current as App).AccessToken);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await ViewModel.LoadContacts();
        }

        private void contactsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectCommand.Execute(this);
        }

        private void contactsListView_Refreshing(object sender, EventArgs e)
        {
            ViewModel.RefreshCommand.Execute(this);
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.SearchCommand.Execute(this);
        }
    }
}