using SQLite;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinDemo.Services
{
    public class ContactService
    {
        private readonly SQLiteAsyncConnection _connection;
        public ContactService()
        {
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            _connection.CreateTableAsync<Contact>();
        }

        public async Task<ObservableCollection<Contact>> GetContacts(string searchQuery = null)
        {
            var query = _connection.Table<Contact>();
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.ToLowerInvariant();
                query = query.Where(x => x.DisplayName.ToLower().Contains(searchQuery));
            }

            var contacts = await query.ToListAsync();

            return new ObservableCollection<Contact>(contacts);
        }

        public async Task<Contact> GetContact(int contactId)
        {
            return await _connection.FindAsync<Contact>(contactId);
        }

        public async Task<Contact> SaveContact(Contact contact)
        {
            if (contact.Id == 0)
            {
                await _connection.InsertAsync(contact);
            }
            else
            {
                await _connection.UpdateAsync(contact);
            }

            return contact;
        }

        public async Task DeleteContact(Contact contact)
        {
            await _connection.DeleteAsync(contact);
        }
    }
}
