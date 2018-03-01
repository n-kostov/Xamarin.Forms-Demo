using SQLite;
using System.IO;
using Windows.Storage;
using Xamarin.Forms;
using XamarinDemo.UWP;

[assembly: Dependency(typeof(SQLiteDb))]
namespace XamarinDemo.UWP
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = ApplicationData.Current.LocalFolder.Path;
            var path = Path.Combine(documentsPath, "ContactsDB.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}
