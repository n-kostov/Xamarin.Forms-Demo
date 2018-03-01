using SQLite;
using System.IO;
using Xamarin.Forms;
using XamarinDemo.Droid;

[assembly: Dependency(typeof(SQLiteDb))]
namespace XamarinDemo.Droid
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "ContactsDB.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}