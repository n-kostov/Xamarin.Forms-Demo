using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinDemo
{
    public partial class MainPage : ContentPage
    {
        public string LabelText { get; set; }
        public MainPage()
        {
            InitializeComponent();

            LabelText = "Binding from code";
            BindingContext = this;
        }
    }
}
