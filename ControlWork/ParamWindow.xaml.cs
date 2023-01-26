using System;
using System.Windows;
using System.Data.SQLite;
using System.Windows.Input;

namespace ControlWork
{
    public partial class ParamWindow : Window
    {
        SQLiteCommand command;
        MainWindow mainWindow;
        public ParamWindow(string category, SQLiteCommand command, MainWindow mainWindow)
        {
            InitializeComponent();
            this.command = command;
            this.mainWindow = mainWindow;

            switch (category)
            {
                case "Смарт-часы":
                    ShowWatchParams();
                    setParams.Click += new RoutedEventHandler(SetSmartWatchesParams);
                    break;
                case "Смартфоны":
                    ShowPhoneParams();
                    setParams.Click += new RoutedEventHandler(SetPhoneParams);
                    break;
                case "Планшеты":
                    ShowTabletParams();
                    setParams.Click += new RoutedEventHandler(SetTabletParams);
                    break;
                case "Беспроводные наушники":
                    ShowEarphoneParams();
                    wireless.IsChecked = true;
                    setParams.Click += new RoutedEventHandler(SetEarphonesParams);
                    break;
                default:
                    ShowEarphoneParams();
                    setParams.Click += new RoutedEventHandler(SetEarphonesParams);
                    break;
            }
        }

        private void SetParams(Product product)
        {
            try
            {
                product.InsertInfo(command);
            }
            catch
            {
                MessageBox.Show("Ошибка! Товар с указанным штрихкодом уже существует");
            }
            mainWindow.LoadGrid();
        }

        private void SetSmartWatchesParams(object sender, RoutedEventArgs e)
        {
            try
            {
                SmartWatch smartWatch = new SmartWatch(productName.Text, barcode.Text,
                        double.Parse(price.Text), int.Parse(watchWithoutCharging.Text),
                        pulseTracking.IsChecked, fitnessTracking.IsChecked, alarm.IsChecked);
                SetParams(smartWatch);
            }
            catch
            {
                MessageBox.Show("Товар не может быть добавлен: не все поля заполнены");
            }
        }

        private void SetPhoneParams(object sender, RoutedEventArgs e)
        {
            try
            {
                Phone phone = new Phone(productName.Text, barcode.Text, double.Parse(price.Text), 
                    OS.Text, int.Parse(RAM.Text), int.Parse(ROM.Text),
                    Convert.ToDouble(screenDiagonal.Text), int.Parse(camera.Text));
                SetParams(phone);
            }
            catch
            {
                MessageBox.Show("Товар не может быть добавлен: не все поля заполнены");
            }
        }

        private void SetTabletParams(object sender, RoutedEventArgs e)
        {
            try
            {
                Tablet tablet = new Tablet(productName.Text, barcode.Text, double.Parse(price.Text), 
                    OS.Text, int.Parse(RAM.Text), int.Parse(ROM.Text), 
                    Convert.ToDouble(screenDiagonal.Text), int.Parse(camera.Text), cellular.IsChecked);
                SetParams(tablet);
            }
            catch
            {
                MessageBox.Show("Товар не может быть добавлен: не все поля заполнены");
            }
        }

        private void SetEarphonesParams(object sender, RoutedEventArgs e)
        {
            try
            {
                if (wireless.IsChecked == true)
                {
                    WirelessEarphones earphones = new WirelessEarphones(productName.Text, 
                        barcode.Text, double.Parse(price.Text), microphone.IsChecked, 
                        int.Parse(sensitivity.Text), int.Parse(earphoneWithoutCharging.Text), 
                        double.Parse(bluetoothVersion.Text));
                    SetParams(earphones);
                }
                else
                {
                    Earphones earphones = new Earphones(productName.Text, barcode.Text,
                        double.Parse(price.Text), microphone.IsChecked, int.Parse(sensitivity.Text));
                    SetParams(earphones);
                }
            }
            catch
            {
                MessageBox.Show("Товар не может быть добавлен: не все поля заполнены");
            }
        }

        private void ShowPhoneParams()
        {
            paramWindow.Title = "Параметры телефона";

            paramName1.Text = "OS";
            paramName2.Text = "RAM";
            paramName3.Text = "ROM";
            paramName4.Text = "Диагональ экрана (Дюймы)";
            paramName5.Text = "Разрешение камеры (Мп)";

            phoneParam.Visibility = Visibility.Visible;
            cellular.IsEnabled = false;
        }

        private void ShowTabletParams()
        {
            paramWindow.Title = "Параметры планшета";

            paramName1.Text = "OS";
            paramName2.Text = "RAM";
            paramName3.Text = "ROM";
            paramName4.Text = "Диагональ экрана (Дюймы)";
            paramName5.Text = "Разрешение камеры (Мп)";
            paramName6.Text = "Сотовая связь";

            phoneParam.Visibility = Visibility.Visible;
        }

        private void ShowWatchParams()
        {
            paramWindow.Title = "Параметры смарт-часов";

            paramName1.Text = "Время работы без зарядки (часы)";
            paramName2.Text = "Будильник";
            paramName3.Text = "Измерение пульса";
            paramName4.Text = "Счётчик шагов";

            watchParam.Visibility = Visibility.Visible;
        }

        private void ShowEarphoneParams()
        {
            paramWindow.Title = "Параметры наушников";

            paramName1.Text = "Чувствительность";
            paramName2.Text = "Микрофон";
            paramName3.Text = "Беспроводные";
            paramName4.Text = "Версия bluetooth";
            paramName5.Text = "Часов работы без зарядки";

            earphoneParam.Visibility = Visibility.Visible;
        }

        private void WirelessChecked(object sender, RoutedEventArgs e)
        {
            if(wireless.IsChecked == true)
            {
                earphoneWithoutCharging.IsEnabled = true;
                bluetoothVersion.IsEnabled = true;
            }
            else 
            {
                earphoneWithoutCharging.IsEnabled = false;
                bluetoothVersion.IsEnabled = false;
            }
        }

        private void number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsNumber(e.Text[0]))
                e.Handled = true;
        }

        private void price_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text[0] == '.')
            {
                foreach (char ch in price.Text)
                    if (ch == '.') e.Handled = true;
            }
            else if (!char.IsNumber(e.Text[0])) e.Handled = true;
        }

        private void bluetooth_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text[0] == '.')
            {
                foreach (char ch in bluetoothVersion.Text)
                    if (ch == '.') e.Handled = true;
            }
            else if (!char.IsNumber(e.Text[0])) e.Handled = true;
        }

        private void diagonal_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text[0] == '.')
            {
                foreach (char ch in screenDiagonal.Text)
                    if (ch == '.') e.Handled = true;
            }
            else if (!char.IsNumber(e.Text[0])) e.Handled = true;
        }

        private void previewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Space)
                e.Handled = true;
        }
    }
}
