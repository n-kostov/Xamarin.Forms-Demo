using SQLite;

namespace XamarinDemo
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
