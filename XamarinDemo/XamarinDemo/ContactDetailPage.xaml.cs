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
    public partial class ContactDetailPage : ContentPage
    {
        private Contact _contact;
        public ContactDetailPage(Contact contact)
        {
            _contact = contact ?? throw new ArgumentNullException();
            BindingContext = _contact;

            InitializeComponent();
        }
    }
}