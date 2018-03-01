using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactDetailPage : ContentPage
    {
        private Contact _contact;
        public event EventHandler<ContactEventArgs> ContactAdded;
        public ContactDetailPage(Contact contact)
        {
            _contact = contact ?? throw new ArgumentNullException();
            BindingContext = _contact;

            InitializeComponent();

            ToolbarItems.Add(new ToolbarItem("Add", "save.png", () => OnContactAdded(this, new ContactEventArgs(_contact)),
                ToolbarItemOrder.Primary));
        }

        private async void OnContactAdded(object sender, ContactEventArgs e)
        {
            ContactAdded?.Invoke(this, e);
            await Navigation.PopAsync();
        }
    }
}