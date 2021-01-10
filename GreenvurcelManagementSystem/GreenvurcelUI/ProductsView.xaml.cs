using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GreenvurcelDAL;

namespace GreenvurcelUI
{
    /// <summary>
    /// Interaction logic for ProductsView.xaml
    /// </summary>
    public partial class ProductsView : UserControl
    {
        private List<Product> products;

        public ProductsView()
        {
            InitializeComponent();
            LoadProducts();
        }   
        private void LoadProducts()
        {
            products = new List<Product>
            {
                new Product
                {
                    Name = "Guy",
                    ProductName = "Guns"

                },
                new Product
                {
                    Name = "Sean",
                    ProductName = "Guns"
                },
                new Product
                {
                   Name = "Dana",
                    ProductName = "Guns"
                },
                new Product
                {
                   Name = "Ron",
                    ProductName = "Guns"
                },
                new Product
                {
                    Name = "Uri",
                    ProductName = "Guns"
                },
                new Product
                {
                    Name = "Baba",
                    ProductName = "Guns"
                },
                new Product
                {
                   Name = "Mama",
                    ProductName = "Guns"
                },
                new Product
                {
                    Name = "Guy",
                    ProductName = "Guns"
                },
            };
            
            Products.ItemsSource = products;

        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow dataGridRow = (DataGridRow)sender; Product product = (Product)dataGridRow.DataContext;
            Name.Text = product.Name;
        }
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedFilter = ((ComboBoxItem)FilterComboBox.SelectedItem).Content.ToString();

            if (selectedFilter == "Name")
            {
                List<Product> filteredCustomers = products.FindAll(product => product.Name == FilterBox.Text);
                Products.ItemsSource = filteredCustomers;
            }
            if (selectedFilter == "Product Name")
            {
                List<Product> filteredCustomers = products.FindAll(product => product.ProductName == FilterBox.Text);
                Products.ItemsSource = filteredCustomers;
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            LoadProducts();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
