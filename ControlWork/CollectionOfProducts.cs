using System;
using System.Collections.ObjectModel;
using System.Data.SQLite;

namespace ControlWork
{
    public class CollectionOfProducts : ObservableCollection<Product>
    {
        SQLiteCommand command { get; set; }

        public CollectionOfProducts(SQLiteCommand command) 
        { 
            this.command = command;
        }

        public void Delete(Product product)
        {
            command.CommandText = $"DELETE FROM SmartWatches WHERE Barcode = '{product.barcode}'; " +
                $"DELETE FROM Phones WHERE Barcode = '{product.barcode}'; " +
                $"DELETE FROM Tablets WHERE Barcode = '{product.barcode}'; " +
                $"DELETE FROM WiredEarphones WHERE Barcode = '{product.barcode}'; " +
                $"DELETE FROM WirelessEarphones WHERE Barcode = '{product.barcode}'; ";
            command.ExecuteNonQuery();
            Remove(product);
        }

        public void LoadSmartWatches()
        {
            command.CommandText = $"SELECT * FROM SmartWatches";
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                SmartWatch smartWatch = new SmartWatch(
                    reader["Title"].ToString(),
                    reader["Barcode"].ToString(),
                    Convert.ToDouble(reader["Price"]),
                    Convert.ToInt32(reader["TimeWithoutCharging"]),
                    Convert.ToBoolean(reader["PulseTracking"]),
                    Convert.ToBoolean(reader["FitnessTracking"]),
                    Convert.ToBoolean(reader["FitnessTracking"]));
                Add(smartWatch);
            }
            reader.Close();
        }

        public void LoadPhones()
        {
            command.CommandText = $"SELECT * FROM Phones";
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Phone phone = new Phone(
                    reader["Title"].ToString(),
                    reader["Barcode"].ToString(),
                    Convert.ToDouble(reader["Price"]),
                    reader["OS"].ToString(),
                    Convert.ToInt32(reader["RAM"]),
                    Convert.ToInt32(reader["ROM"]),
                    Convert.ToDouble(reader["ScreenDiagonal"]),
                    Convert.ToInt32(reader["Camera"]));
                Add(phone);
            }
            reader.Close();
        }

        public void LoadTablets()
        {
            command.CommandText = $"SELECT * FROM Tablets";
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Tablet tablet = new Tablet(
                    reader["Title"].ToString(),
                    reader["Barcode"].ToString(),
                    Convert.ToDouble(reader["Price"]),
                    reader["OS"].ToString(),
                    Convert.ToInt32(reader["RAM"]),
                    Convert.ToInt32(reader["ROM"]),
                    Convert.ToDouble(reader["ScreenDiagonal"]),
                    Convert.ToInt32(reader["Camera"]),
                    Convert.ToBoolean(reader["Cellular"]));
                Add(tablet);
            }
            reader.Close();
        }

        public void LoadWiredEarphones()
        {
            command.CommandText = $"SELECT * FROM WiredEarphones";
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Earphones earphones = new Earphones(
                    reader["Title"].ToString(),
                    reader["Barcode"].ToString(),
                    Convert.ToDouble(reader["Price"]),
                    Convert.ToBoolean(reader["Microphone"]),
                    Convert.ToInt32(reader["Sensitivity"]));
                Add(earphones);
            }
            reader.Close();
        }

        public void LoadWirelessEarphones()
        {
            command.CommandText = $"SELECT * FROM WirelessEarphones";
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                WirelessEarphones earphones = new WirelessEarphones(
                    reader["Title"].ToString(),
                    reader["Barcode"].ToString(),
                    Convert.ToDouble(reader["Price"]),
                    Convert.ToBoolean(reader["Microphone"]),
                    Convert.ToInt32(reader["Sensitivity"]),
                    Convert.ToInt32(reader["TimeWithoutCharging"]),
                    Convert.ToDouble(reader["BluetoothVersion"]));
                Add(earphones);
            }
            reader.Close();
        }
    }
}
