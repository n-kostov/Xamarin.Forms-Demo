using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
            await Application.Current.MainPage.Navigation.PushModalAsync(new IntroductionPage());
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}