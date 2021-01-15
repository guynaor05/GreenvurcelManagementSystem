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
        private List<CustomerProduct> products;

        public ProductsView()
        {
            InitializeComponent();
            LoadCustomerProducts();
        }   
        private void LoadCustomerProducts()
        {
            //products = new List<Sell>
            //{
            //    new Sell
            //    {
            //         = "Guy",
            //        SellName = "Guns"

            //    },
            //    new Sell
            //    {
            //        Name = "Sean",
            //        SellName = "Guns"
            //    },
            //    new Sell
            //    {
            //       Name = "Dana",
            //        SellName = "Guns"
            //    },
            //    new Sell
            //    {
            //       Name = "Ron",
            //        SellName = "Guns"
            //    },
            //    new Sell
            //    {
            //        Name = "Uri",
            //        SellName = "Guns"
            //    },
            //    new Sell
            //    {
            //        Name = "Baba",
            //        SellName = "Guns"
            //    },
            //    new Sell
            //    {
            //       Name = "Mama",
            //        SellName = "Guns"
            //    },
            //    new Sell
            //    {
            //        Name = "Guy",
            //        SellName = "Guns"
            //    },
            //};
            
            //Products.ItemsSource = products;

        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow dataGridRow = (DataGridRow)sender; CustomerProduct product = (CustomerProduct)dataGridRow.DataContext;
            Name.Text = product.ProductName;
        }
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedFilter = ((ComboBoxItem)FilterComboBox.SelectedItem).Content.ToString();

            if (selectedFilter == "Name")
            {
                List<CustomerProduct> filteredCustomers = products.FindAll(product => product.ProductName == FilterBox.Text);
                Products.ItemsSource = filteredCustomers;
            }
            if (selectedFilter == "Product Name")
            {
                List<CustomerProduct> filteredCustomers = products.FindAll(product => product.ProductName == FilterBox.Text);
                Products.ItemsSource = filteredCustomers;
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            LoadCustomerProducts();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            CustomerProduct CustomerProduct = new CustomerProduct
            {
                CustomerID = long.Parse(Name.Text),
                ProductName = ProductName.Text,
                CategoryName = Category.Text
            };
            CustomerProductsContext.Instance.InsertCustomerProduct(CustomerProduct);
        }
    }
}
