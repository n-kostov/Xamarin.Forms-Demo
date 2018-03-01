using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinDemo.Services;
using XamarinDemo.ViewModels;

namespace XamarinDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactDetailPage : ContentPage
    {
        public ContactDetailPage(ContactViewModel contact)
        {
            if(contact == null)
            {
                throw new ArgumentNullException();
            }

            IPageService pageService = new PageService();
            string accessToken = (Application.Current as App).AccessToken;
            ContactEditViewModel contactVM = new ContactEditViewModel(Navigation, pageService, accessToken)
            {
                Contact = contact
            };
            BindingContext = contactVM;

            InitializeComponent();
        }
    }
}