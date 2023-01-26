using System.Windows;
using System.Windows.Controls;
using System.Data.SQLite;

namespace ControlWork
{
    public partial class MainWindow : Window
    { 
        SQLiteConnection connection = new SQLiteConnection("Data source = Products.db");
        SQLiteCommand command = new SQLiteCommand();
        CollectionOfProducts products;
        string category;
        
        public MainWindow()
        {
            InitializeComponent();
            connection.Open();
            command.Connection = connection;
            products = new CollectionOfProducts(command);
            dataGrid.ItemsSource = products;
            LoadGrid();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            if (category == "Все товары" || category == null)
            {
                MessageBox.Show("Выберите категорию товара");
                return;
            }
            TreeViewItem selectedItem = (TreeViewItem)treeView.SelectedItem;
            ParamWindow window = new ParamWindow(selectedItem.Header.ToString(), command, this);
            window.Show();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null) return;
            Product product = (Product)dataGrid.SelectedItem;
            products.Delete(product);
            LoadGrid();
        }

        private void SelectedItemChanged(object sender, RoutedEventArgs e) => LoadGrid();

        public void LoadGrid()
        {
            products.Clear();
            for (int i = 3; i < 16; i++) 
                dataGrid.Columns[i].Visibility = Visibility.Collapsed;

            if (treeView.SelectedItem == null) return;

            TreeViewItem selectedItem = (TreeViewItem)treeView.SelectedItem;
            category = selectedItem.Header.ToString();
            switch (category)
            {
                case "Все товары":
                    products.LoadTablets();
                    products.LoadPhones();
                    products.LoadSmartWatches();
                    products.LoadWiredEarphones();
                    products.LoadWirelessEarphones();
                    break;
                case "Смартфоны":
                    products.LoadPhones(); 
                    for (int i = 3; i < 8; i++) 
                        dataGrid.Columns[i].Visibility = Visibility.Visible;
                    break;
                case "Планшеты":
                    products.LoadTablets();
                    for (int i = 3; i < 9; i++) 
                        dataGrid.Columns[i].Visibility = Visibility.Visible;
                    break;
                case "Смарт-часы":
                    products.LoadSmartWatches();
                    for (int i = 9; i < 13; i++) 
                        dataGrid.Columns[i].Visibility = Visibility.Visible;
                    break;
                case "Наушники":
                    products.LoadWiredEarphones();
                    products.LoadWirelessEarphones();
                    for (int i = 12; i < 16; i++) 
                        dataGrid.Columns[i].Visibility = Visibility.Visible;
                    for (int i = 12; i < 14; i++)
                        dataGrid.Columns[i].IsReadOnly = true;
                    break;
                case "Беспроводные наушники":
                    products.LoadWirelessEarphones();
                    for (int i = 12; i < 16; i++) 
                        dataGrid.Columns[i].Visibility = Visibility.Visible;
                    for (int i = 12; i < 14; i++)
                        dataGrid.Columns[i].IsReadOnly = false;
                    break;
                case "Проводные наушники":
                    products.LoadWiredEarphones();
                    for (int i = 14; i < 16; i++) 
                        dataGrid.Columns[i].Visibility = Visibility.Visible;
                    break;
            }          
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            foreach (Product product in products)
                product.UpdateInfo(command);
        }
    }
}
