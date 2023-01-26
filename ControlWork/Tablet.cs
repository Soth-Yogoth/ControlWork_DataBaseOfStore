using System.Data.SQLite;

namespace ControlWork
{
    internal class Tablet : Phone
    {
        public bool? cellular { get; set; }

        public Tablet(string title, string barcode, double price,
            string OS, int RAM, int ROM, double screenDiagonal, int camera, bool? cellular)
            : base(title, barcode, price, OS, RAM, ROM, screenDiagonal, camera)
        {
            this.title = title;
            this.barcode = barcode;
            this.price = price;
            this.OS = OS;
            this.RAM = RAM;
            this.ROM = ROM;
            this.camera = camera;
            this.screenDiagonal = screenDiagonal;
            this.cellular = cellular;
        }

        public override void InsertInfo(SQLiteCommand command)
        {
            command.CommandText = $"INSERT INTO Tablets" +
                $"(Barcode, Title, Price, OS, RAM, ROM, Camera, ScreenDiagonal, Cellular) " +
                $"VALUES ({barcode}, '{title}', {price}, '{OS}', " +
                $"{RAM}, {ROM}, {camera}, {screenDiagonal}, {cellular})";
            command.ExecuteNonQuery();
        }
        public override void UpdateInfo(SQLiteCommand command) 
        {
            command.CommandText = $"UPDATE Tablets SET Title='{title}', Price={price}," +
                $"OS='{OS}', RAM={RAM}, ROM={ROM}, Camera={camera}, " +
                $"ScreenDiagonal={screenDiagonal}, Cellular={cellular} " +
                $"WHERE Barcode = {barcode}";
            command.ExecuteNonQuery();
        }
    }
}
