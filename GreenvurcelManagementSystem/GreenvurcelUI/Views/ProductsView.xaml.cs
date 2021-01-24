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

            CustomerProductsContext.Instance.ProdcutAdded += Instance_ProdcutAdded; ;

            CustomerContext.Instance.CustomerRemoved += Instance_CustomerRemoved;
        }

        private void Instance_CustomerRemoved()
        {
            LoadCustomerProducts();
        }

        private void Instance_ProdcutAdded()
        {
            LoadCustomerProducts();
        }

        private void LoadCustomerProducts()
        {
            products = CustomerProductsContext.Instance.LoadCustomerProducts();
            if (products == null)
            {
                MessageBox.Show("Unable to connect to databse");
            }
            else
            {
                Products.ItemsSource = products;
            }

        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow dataGridRow = (DataGridRow)sender;
            CustomerProduct product = (CustomerProduct)dataGridRow.DataContext;
            CustomerID.Text = product.CustomerID.ToString();
        }
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedFilter = ((ComboBoxItem)FilterComboBox.SelectedItem).Content.ToString();

            if (selectedFilter == "CustomerID")
            {
                List<CustomerProduct> filteredCustomers = products.FindAll(product => product.CustomerID == long.Parse(FilterBox.Text));
                Products.ItemsSource = filteredCustomers;
            }
            if (selectedFilter == "Product Name")
            {
                List<CustomerProduct> filteredCustomers = products.FindAll(product => product.ProductName == FilterBox.Text);
                Products.ItemsSource = filteredCustomers;
            }
            if (selectedFilter == "Category Name")
            {
                List<CustomerProduct> filteredCustomers = products.FindAll(product => product.CategoryName == FilterBox.Text);
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
                CustomerID = long.Parse(CustomerID.Text),
                ProductName = ProductName.Text,
                CategoryName = Category.Text
            };
            CustomerProductsContext.Instance.InsertCustomerProduct(CustomerProduct);
            CustomerID.Text = "";
            ProductName.Text = "";
            Category.Text = "";
            MessageBox.Show("Product added Successfully");
        }

        private void DataGridRow_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void DeleteCustomerProduct(object sender, RoutedEventArgs e)
        {
            CustomerProduct customerProduct = (CustomerProduct)Products.SelectedItem;
            bool succeeded = CustomerProductsContext.Instance.DeleteCustomerProduct(customerProduct._id);
            if (!succeeded)
            {
                MessageBox.Show("Unable to connect to databse");
            }
            else
            {
                LoadCustomerProducts();
                MessageBox.Show("Customer deleted Successfully");
            }
        }
    }
}
