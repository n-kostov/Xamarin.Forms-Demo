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
    public partial class IntroductionPage : CarouselPage
    {
        public IntroductionPage()
        {
            InitializeComponent();
        }

        private async void GetStarted_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}