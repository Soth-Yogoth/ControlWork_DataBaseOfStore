using System.Data.SQLite;

namespace ControlWork
{
   abstract public class Product
   {
        public string title { get; set; }
        public string barcode { get; set; }
        public double price { get; set; }

        abstract public void InsertInfo(SQLiteCommand command);
        abstract public void UpdateInfo(SQLiteCommand command);
   }
}
