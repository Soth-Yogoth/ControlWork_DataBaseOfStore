using System.Data.SQLite;

namespace ControlWork
{
    internal class Phone : Product 
    {
        public string OS { get; set; }
        public int RAM { get; set; }
        public int ROM { get; set; }
        public int camera { get; set; }
        public double screenDiagonal { get; set; }

        public Phone(string title, string barcode, double price,
            string OS, int RAM, int ROM, double screenDiagonal, int camera)
        {
            this.title = title;
            this.OS = OS;
            this.barcode = barcode;
            this.price = price;
            this.RAM = RAM;
            this.ROM = ROM;
            this.camera = camera;
            this.screenDiagonal = screenDiagonal;
        }
        public override void InsertInfo(SQLiteCommand command)
        {
            command.CommandText = $"INSERT INTO Phones " +
                $"(Barcode, Title, Price, OS, RAM, ROM, Camera, ScreenDiagonal) " +
                $"VALUES ('{barcode}', '{title}', {price}, '{OS}', " +
                $"{RAM}, {ROM}, {camera}, {screenDiagonal}); ";
            command.ExecuteNonQuery();
        }
        public override void UpdateInfo(SQLiteCommand command) 
        {
            command.CommandText = $"UPDATE Phones SET Title='{title}', Price={price}, " +
                $"OS='{OS}', RAM={RAM}, ROM={ROM}, Camera={camera}, ScreenDiagonal={screenDiagonal} " +
                $"WHERE Barcode = '{barcode}'";
            command.ExecuteNonQuery();
        }
    }
}
