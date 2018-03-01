using System.Windows.Input;
using Xamarin.Forms;
using XamarinDemo.Services;

namespace XamarinDemo.ViewModels
{
    public class ContactEditViewModel: BaseViewModel
    {
        private readonly INavigation _navigation;
        private readonly IPageService _pageService;
        private readonly ContactService _contactService;

        public ContactViewModel Contact { get; set; }
        public ICommand SaveCommand { get; set; }
        public ContactEditViewModel(INavigation navigation, IPageService pageService, string accessToken)
        {
            _navigation = navigation;
            _pageService = pageService;

            _contactService = new ContactService(accessToken);

            Contact = new ContactViewModel();

            SaveCommand = new Command(Save);
        }

        private async void Save()
        {
            if (string.IsNullOrWhiteSpace(Contact.DisplayName))
            {
                await _pageService.DisplayAlert("Warning", "Please enter the name!", "OK");
            }
            else
            {
                Contact contact = new Contact
                {
                    Id = Contact.Id,
                    DisplayName = Contact?.DisplayName,
                    Name = Contact?.Name,
                    Description = Contact?.Description,
                    PhoneNumber = Contact?.PhoneNumber,
                    DateOfBirth = Contact.DateOfBirth,
                    IsColleague = Contact.IsColleague
                };

                await _contactService.SaveContact(contact);

                await _navigation.PopAsync();
            }
        }
    }
}
