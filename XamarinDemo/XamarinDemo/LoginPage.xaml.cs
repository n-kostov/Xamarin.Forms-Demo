using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinDemo.Services;

namespace XamarinDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly LoginService _loginService = new LoginService();
        
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            indicator.IsRunning = true;
            try
            {
                string accessToken = await _loginService.Login(usernameEntry.Text, passwordEntry.Text);
                App currentApp = Application.Current as App;
                currentApp.AccessToken = accessToken;

                await Navigation.PopModalAsync();
                if (!currentApp.HasReadIntroduction)
                {
                    await Application.Current.MainPage.Navigation.PushModalAsync(new IntroductionPage());
                }
            }
            catch (Exception ex)
            {
                errorLabel.Text = ex.Message;
            }
            finally
            {
                indicator.IsRunning = false;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}