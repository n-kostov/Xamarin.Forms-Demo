using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailExamplePageMaster : ContentPage
    {
        public ListView ListView;

        public MasterDetailExamplePageMaster()
        {
            InitializeComponent();

            BindingContext = new MasterDetailExamplePageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MasterDetailExamplePageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterDetailExamplePageMenuItem> MenuItems { get; set; }

            public MasterDetailExamplePageMasterViewModel()
            {
                MenuItems = new ObservableCollection<MasterDetailExamplePageMenuItem>(new[]
                {
                    new MasterDetailExamplePageMenuItem { Id = 0, Title = "Page 1" },
                    new MasterDetailExamplePageMenuItem { Id = 1, Title = "Page 2" },
                    new MasterDetailExamplePageMenuItem { Id = 2, Title = "Page 3" },
                    new MasterDetailExamplePageMenuItem { Id = 3, Title = "Page 4" },
                    new MasterDetailExamplePageMenuItem { Id = 4, Title = "Page 5" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}