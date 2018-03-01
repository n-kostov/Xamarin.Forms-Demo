using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhoneDialPage : ContentPage
    {
        public PhoneDialPage()
        {
            InitializeComponent();
        }

        private void NumberButton_Clicked(object sender, EventArgs e)
        {
            phoneNumberLabel.Text += (sender as Button).Text;
        }

        private void Backspace_Clicked(object sender, EventArgs e)
        {
            if (phoneNumberLabel.Text.Length > 0)
            {
                phoneNumberLabel.Text = phoneNumberLabel.Text.Substring(0, phoneNumberLabel.Text.Length - 1);
            }
        }
    }
}