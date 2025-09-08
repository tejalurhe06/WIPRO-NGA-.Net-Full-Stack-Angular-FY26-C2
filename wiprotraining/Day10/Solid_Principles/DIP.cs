using System;

namespace DIP
{

    //Without DIP
    // public class MySqlDatabase
    // {
    //     public void Save() =>
    //     Console.WriteLine("Saving to MySQL");
    // }

    // public class DataService
    // {
    //     private MySqlDatabase db = new MySqlDatabase();
    //     public void SaveData() => db.Save;
    // }

    //With DIP

    public interface IDatabase
    {
        void Save();
    }

    public class MySqlDatabase : IDatabase
    {
        public void Save()
        {
            Console.WriteLine("Saving to MySql");
        }
    }

    public class DataService
    {
        private IDatabase db;

        public DataService(IDatabase db)
        {
            this.db = db;
        }

        public void SaveData() => db.Save();
    }
}