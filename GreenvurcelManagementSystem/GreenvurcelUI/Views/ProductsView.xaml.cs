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

            ReportsView.ShowProdutsRequest += ReportsView_ShowProdutsRequest;

            ReportsView.AddProductRequest += ReportsView_AddProductRequest;
        }

        private void ReportsView_AddProductRequest(long obj)
        {
            CustomerID.Text = obj.ToString();

        }

        private void ReportsView_ShowProdutsRequest(long obj)
        {
            FilterComboBox.SelectedItem = "CustomerID";
            FilterBox.Text = obj.ToString();
            List<CustomerProduct> filteredCustomers = products.FindAll(product => product.CustomerID == long.Parse(FilterBox.Text));
            Products.ItemsSource = filteredCustomers;


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
                foreach(CustomerProduct product in products)
                {
                    Customer CustomerDeitals = CustomerContext.Instance.LoadCustomerByIdForProduct(product.CustomerID, out bool succeeded);
                    product.FirstName = CustomerDeitals.FirstName;
                    product.LastName = CustomerDeitals.LastName;
                }
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
                if (FilterBox.Text != "")
                {
                    if (ValidateNumber(FilterBox.Text))
                    {
                        List<CustomerProduct> filteredCustomers = products.FindAll(product => product.CustomerID == long.Parse(FilterBox.Text));
                        Products.ItemsSource = filteredCustomers;
                    }
                    else
                    {
                        Products.ItemsSource = null;
                    }
                }
                else
                {
                    Products.ItemsSource = null;
                }
            }
            if (selectedFilter == "Product")
            {
                if (FilterBox.Text != "")
                {
                    if (ValidateLetter(FilterBox.Text))
                    {
                        List<CustomerProduct> filteredCustomers = products.FindAll(product => product.ProductName.Contains(FilterBox.Text));
                        Products.ItemsSource = filteredCustomers;
                    }
                    else
                    {
                        Products.ItemsSource = null;
                    }
                }
                else
                {
                    Products.ItemsSource = null;
                }
            }
            if (selectedFilter == "Category Name")
            {
                if(FilterBox.Text != "")
                {
                    if (ValidateLetter(FilterBox.Text))
                    {
                        List<CustomerProduct> filteredCustomers = products.FindAll(product => product.CategoryName.Contains(FilterBox.Text));
                        Products.ItemsSource = filteredCustomers;
                    }
                    else
                    {
                        Products.ItemsSource = null;
                    }
                }
                else
                {
                    Products.ItemsSource = null;
                }
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            LoadCustomerProducts();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            if(CustomerID.Text != "")
            {
                if (CustomerContext.Instance.CheckIfIdExist(long.Parse(CustomerID.Text)))
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
                else
                {
                    MessageBox.Show("Customer Id does not exist");
                }
            }
            else
            {
                MessageBox.Show("Customer Id was not given");
            }
        }

        private void DeleteCustomerProduct(object sender, RoutedEventArgs e)
        {
            CustomerProduct customerProduct = (CustomerProduct)Products.SelectedItem;
            if (MessageBox.Show($"Are you sure you want to delete this Product?", "Delete Product", MessageBoxButton.OKCancel) != MessageBoxResult.Cancel)
            {
                bool succeeded = CustomerProductsContext.Instance.DeleteCustomerProduct(customerProduct._id);
                if (!succeeded)
                {
                    MessageBox.Show("Unable to connect to databse");
                }
                else
                {
                    LoadCustomerProducts();
                    MessageBox.Show("Product deleted Successfully");
                }
            }
        }

        private void CustomerID_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = CustomerID.Text;
            if (string.IsNullOrWhiteSpace(text))
            {
                AddProduct.IsEnabled = false;
            }

            foreach (char c in text)
            {
                if (char.IsDigit(c))
                {
                    AddProduct.IsEnabled = true;
                }
                else
                {
                    AddProduct.IsEnabled = false;
                }
            }
        }
        private bool ValidateLetter(string text)
        {
            foreach (char c in text)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }

            return true;
        }
        private bool ValidateNumber(string text)
        {
            foreach (char c in text)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
