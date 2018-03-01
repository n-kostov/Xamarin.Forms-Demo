using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinDemo.Services
{
    public interface IPageService
    {
        Task DisplayAlert(string title, string message, string ok);

        Task<bool> DisplayAlert(string title, string message, string accept, string cancel);
    }
}
