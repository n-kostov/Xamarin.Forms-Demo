using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormPage : ContentPage
    {
        public FormPage()
        {
            InitializeComponent();
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
                DisplayAlert("Togled", e.Value.ToString(), "OK");
        }

        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            DisplayAlert("Slider value changed", e.NewValue.ToString(), "OK");
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            label.Text = "Completed";
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            label.Text = e.NewTextValue;
        }
    }
}