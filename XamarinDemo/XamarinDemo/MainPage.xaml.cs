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
        public MainPage()
        {
            InitializeComponent();

            var stack = new StackLayout();
            Content = stack;

            stack.Children.Add(new Label()
            {
                Text = "From code",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Yellow,
                FontSize = 30,
                IsVisible = true
            });

            Padding = Device.OnPlatform(iOS: new Thickness(), 
                Android: new Thickness(0, 10, 0, 0), 
                WinPhone: new Thickness(0, 20, 0, 0));

            Thickness padding;
            switch(Device.RuntimePlatform)
            {
                case Device.Android:
                    padding = new Thickness(0, 10, 0, 0);
                    break;
                default:
                    padding = new Thickness();
                    break;
            }

            Padding = padding;
        }
    }
}
