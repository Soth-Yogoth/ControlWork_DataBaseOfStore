using System.Data.SQLite;
using System.Linq.Expressions;

namespace ControlWork
{
    public class SmartWatch : Product
    {
        public int timeWithoutCharging { get; set; }
        public bool? pulseTracking { get; set; }
        public bool? fitnessTracking { get; set; }
        public bool? alarm { get; set; }

        public SmartWatch(string title, string barcode, double price,
            int timeWithoutCharging, bool?   pulseTracking, bool? fitnessTracking, bool? alarm)
        {
            this.title = title;
            this.barcode = barcode;
            this.price = price;
            this.timeWithoutCharging = timeWithoutCharging;
            this.pulseTracking = pulseTracking;
            this.fitnessTracking = fitnessTracking;
            this.alarm = alarm;
        }

        public override void InsertInfo(SQLiteCommand command)
        {
            command.CommandText = $"INSERT INTO SmartWatches " +
                $"(Barcode, Title, Price, TimeWithoutCharging, PulseTracking, FitnessTracking, Alarm) " +
                $"VALUES ('{barcode}', '{title}', {price}, {timeWithoutCharging}, " +
                $"{pulseTracking}, {fitnessTracking}, {alarm});";
            command.ExecuteNonQuery();
        }

        public override void UpdateInfo(SQLiteCommand command)
        {
            command.CommandText = $"UPDATE SmartWatches SET Title='{title}', Price={price}," +
                $"TimeWithoutCharging={timeWithoutCharging}, PulseTracking={pulseTracking}, " +
                $"FitnessTracking={fitnessTracking}, Alarm={alarm} " +
                $"WHERE Barcode = '{barcode}'";
            command.ExecuteNonQuery();
        }
    }
}
