using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XamarinDemo
{
    public partial class App : Application
    {
        private static readonly string HasReadIntroductionString = "HasReadIntroduction";
        public App()
        {
            InitializeComponent();

            MainPage = new HomePage();
            MainPage.Navigation.PushModalAsync(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public bool HasReadIntroduction
        {
            get
            {
                if (Properties.ContainsKey(HasReadIntroductionString))
                {
                    return (bool)Properties[HasReadIntroductionString];
                }

                return false;
            }
            set
            {
                Properties[HasReadIntroductionString] = value;
            }
        }
    }
}
