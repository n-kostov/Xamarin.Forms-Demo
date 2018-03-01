using Newtonsoft.Json;
using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinDemo.Services
{
    public class ContactService
    {
        private static readonly string NewContactsUrl = "http://jsonplaceholder.typicode.com/users";
        private readonly SQLiteAsyncConnection _connection;
        private readonly string _accessToken;
        public ContactService(string accessToken)
        {
            _accessToken = accessToken;
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

        public async Task<ObservableCollection<Contact>> FindNewContacts(string searchQuery)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);
                string response = await client.GetStringAsync(NewContactsUrl);
                List<Contact> contacts = JsonConvert.DeserializeObject<List<Contact>>(response);
                foreach (var c in contacts)
                {
                    c.Id = 0;
                    c.DisplayName = c.Name;
                }

                searchQuery = searchQuery.ToLowerInvariant();
                var queriedContacts = contacts.Where(c => c.DisplayName.ToLowerInvariant().Contains(searchQuery));

                return new ObservableCollection<Contact>(queriedContacts);
            }
        }
    }
}
