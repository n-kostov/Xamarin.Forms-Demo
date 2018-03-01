using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DateCell : ViewCell
    {
        public static readonly BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateTime), typeof(DateCell), DateTime.Today);
        public DateTime Date
        {
            get
            {
                return (DateTime)GetValue(DateProperty);
            }
            set
            {
                if (value != null)
                {
                    SetValue(DateProperty, value);
                }
            }
        }
        public DateCell()
        {
            BindingContext = this;

            InitializeComponent();
        }
    }
}