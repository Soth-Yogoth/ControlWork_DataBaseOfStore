using System.Data.SQLite;

namespace ControlWork
{
    internal class Earphones : Product
    {
        public bool? microphone { get; set; }
        public int sensitivity { get; set; }

        public Earphones(string title, string barcode, double price,
            bool? microphone, int sensitivity)
        {
            this.title = title;
            this.barcode = barcode;
            this.price = price;
            this.microphone = microphone;
            this.sensitivity = sensitivity;
        }
        public override void InsertInfo(SQLiteCommand command)
        {
            command.CommandText = $"INSERT INTO WiredEarphones" +
                $"(Barcode, Title, Price, Microphone, Sensitivity) " +
                $"VALUES ({barcode}, '{title}', {price}, " +
                $"{microphone}, {sensitivity})";
            command.ExecuteNonQuery();
        }
        public override void UpdateInfo(SQLiteCommand command) 
        {
            command.CommandText = $"UPDATE WiredEarphones SET Title='{title}', Price={price}," +
                $" Microphone={microphone}, Sensitivity={sensitivity} " +
                $"WHERE Barcode = {barcode}";
            command.ExecuteNonQuery();
        }
    }

    internal class WirelessEarphones : Earphones
    {
        public int timeWithoutCharging { get; set; }
        public double bluetoothVersion { get; set; }

        public WirelessEarphones(string title, string barcode, double price,
            bool? microphone, int sensitivity, int timeWithoutCharging, double bluetoothVersion)
            : base(title, barcode, price, microphone, sensitivity)
        {
            this.title = title;
            this.barcode = barcode;
            this.price = price;
            this.microphone = microphone;
            this.sensitivity = sensitivity;
            this.timeWithoutCharging = timeWithoutCharging;
            this.bluetoothVersion = bluetoothVersion;
        }
        public override void InsertInfo(SQLiteCommand command)
        {
            command.CommandText = $"INSERT INTO WirelessEarphones" +
                $"(Barcode, Title, Price, TimeWithoutCharging, Microphone, Sensitivity, BluetoothVersion)" +
                $"VALUES ({barcode}, '{title}', {price}, {timeWithoutCharging}," +
                $" {microphone}, {sensitivity}, {bluetoothVersion})";
            command.ExecuteNonQuery();
        }
        public override void UpdateInfo(SQLiteCommand command) 
        {
            command.CommandText = $"UPDATE WirelessEarphones SET Title='{title}', Price={price}," +
                $"TimeWithoutCharging={timeWithoutCharging}, Microphone={microphone}, " +
                $"Sensitivity={sensitivity}, BluetoothVersion={bluetoothVersion} " +
                $"WHERE Barcode = {barcode}";
            command.ExecuteNonQuery();
        }
    }
}
