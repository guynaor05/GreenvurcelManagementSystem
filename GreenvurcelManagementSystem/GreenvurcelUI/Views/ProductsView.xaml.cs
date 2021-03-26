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
using ClosedXML.Excel;
using GreenvurcelDAL;
using System.IO;
using Microsoft.Win32;
using System.Linq;
using WPFCustomMessageBox;
using System.Collections.ObjectModel;

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
                CustomMessageBox.Show("Unable to connect to databse");
            }
            else
            {
                foreach (CustomerProduct product in products)
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
                    if (ValidateSpaceLetterAndNumber(FilterBox.Text))
                    {
                        string filterLower = Lowercase(FilterBox.Text);
                        List<CustomerProduct> filteredProducts = products.FindAll(product => product.ProductName != null && product.ProductName.ToLower().Contains(filterLower));
                        Products.ItemsSource = filteredProducts;
                    }
                    else
                    {
                        List<CustomerProduct> filteredProducts = products.FindAll(product => product.ProductName.Contains(FilterBox.Text));
                        Products.ItemsSource = filteredProducts;
                    }
                }
                else
                {
                    Products.ItemsSource = null;
                }
            }
            if (selectedFilter == "Category Name")
            {
                if (FilterBox.Text != "")
                {
                    if (ValidateSpaceLetterAndNumber(FilterBox.Text))
                    {
                        string filterLower = Lowercase(FilterBox.Text);
                        List<CustomerProduct> filteredProducts = products.FindAll(product => product.CategoryName != null && product.CategoryName.ToLower().Contains(filterLower));
                        Products.ItemsSource = filteredProducts;
                    }
                    else
                    {
                        List<CustomerProduct> filteredProducts = products.FindAll(product => product.CategoryName.Contains(FilterBox.Text));
                        Products.ItemsSource = filteredProducts;
                    }
                }
                else
                {
                    Products.ItemsSource = null;
                }
            }
            if (selectedFilter == "Object")
            {
                if (FilterBox.Text != "")
                {
                    if (FilterBox.Text.ToLower() == "true" || FilterBox.Text.ToLower() == "false")
                    {
                        List<CustomerProduct> filteredCustomers = new List<CustomerProduct>();
                        string filterUpper = UppercaseFirst(FilterBox.Text);
                        List<CustomerProduct> filteredCustomersUpper = products.FindAll(product => product.IsObject.Equals(bool.Parse(filterUpper)));
                        foreach (CustomerProduct filteredCustomerUpper in filteredCustomersUpper)
                        {
                            filteredCustomers.Add(filteredCustomerUpper);
                        }
                        string filterLower = LowercaseFirst(FilterBox.Text);
                        List<CustomerProduct> filteredCustomersLower = products.FindAll(product => product.IsObject.Equals(bool.Parse(filterLower)));
                        foreach (CustomerProduct filteredCustomerLower in filteredCustomersLower)
                        {
                            filteredCustomers.Add(filteredCustomerLower);
                        }
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
            if (CustomerID.Text != "")
            {
                if (CustomerContext.Instance.CheckIfIdExist(long.Parse(CustomerID.Text)))
                {
                    bool isChecked = (bool)objectCheckBox.IsChecked;
                    CustomerProduct CustomerProduct = new CustomerProduct
                    {
                        CustomerID = long.Parse(CustomerID.Text),
                        ProductName = ProductName.Text,
                        CategoryName = Category.Text,
                        IsObject = isChecked
                    };
                    var sad = objectCheckBox;
                    CustomerProductsContext.Instance.InsertCustomerProduct(CustomerProduct);
                    CustomerID.Text = "";
                    ProductName.Text = "";
                    Category.Text = "";
                    objectCheckBox.IsChecked = false;
                    CustomMessageBox.Show("Product added Successfully");
                }
                else
                {
                    CustomMessageBox.Show("Customer Id does not exist");
                }
            }
            else
            {
                CustomMessageBox.Show("Customer Id was not given");
            }
        }

        private void DeleteCustomerProduct(object sender, RoutedEventArgs e)
        {
            try
            {
                CustomerProduct customerProduct = (CustomerProduct)Products.SelectedItem;
                if(customerProduct != null)
                {
                    if (CustomMessageBox.Show($"Are you sure you want to delete this Product?", "Delete Product", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        bool succeeded = CustomerProductsContext.Instance.DeleteCustomerProduct(customerProduct._id);
                        if (!succeeded)
                        {
                            CustomMessageBox.Show("Unable to connect to databse");
                        }
                        else
                        {
                            LoadCustomerProducts();
                            CustomMessageBox.Show("Product deleted Successfully");
                        }
                    }
                }
            }
            catch
            {
                CustomMessageBox.Show("Cant delete product");
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
        private bool ValidateSpaceLetterAndNumber(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }
            foreach (char c in text)
            {
                if (!char.IsLetter(c) && (!char.IsDigit(c)))
                {
                    return false;
                }
            }
            return true;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Products.ItemsSource != null)
            {
                ListToExcel(Products.ItemsSource.Cast<CustomerProduct>().ToList());
            }
            else
            {
                CustomMessageBox.Show("Cant export without products");
            }
        }

        private void ListToExcel(List<CustomerProduct> list)
        {
            var workbook = new XLWorkbook();
            workbook.AddWorksheet("Products");
            MakeSheet(workbook, list);
            SaveFileDialog(workbook);
        }
        private void MakeSheet(IXLWorkbook workbook, List<CustomerProduct> list)
        {
            var sheet = workbook.Worksheet("Products");
            sheet.Cell("A1").Value = "Customer ID";
            sheet.Cell("B1").Value = "First Name";
            sheet.Cell("C1").Value = "Last Name";
            sheet.Cell("D1").Value = "Product Name";
            sheet.Cell("E1").Value = "Category Name";
            sheet.Cell("F1").Value = "Is Object";
            int counter = 2;
            foreach (CustomerProduct item in list)
            {
                sheet.Cell("A" + counter.ToString()).Value = item.CustomerID;
                sheet.Cell("B" + counter.ToString()).Value = item.FirstName;
                sheet.Cell("C" + counter.ToString()).Value = item.LastName;
                sheet.Cell("D" + counter.ToString()).Value = item.ProductName;
                sheet.Cell("E" + counter.ToString()).Value = item.CategoryName;
                sheet.Cell("F" + counter.ToString()).Value = item.IsObject;
                counter++;
            }
        }
        private void SaveFileDialog(IXLWorkbook workbook)
        {
            SaveFileDialog dlg = new SaveFileDialog
            {
                FileName = "Products", // Default file name
                DefaultExt = ".xlsx", // Default file extension
                Filter = "Execl files (*.xlsx)|*.xlsx" // Filter files by extension
            };

            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;

                workbook.SaveAs(System.IO.Path.GetFullPath(dlg.FileName.ToString()));
            }
        }
        static string Lowercase(string filterBoxText)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(filterBoxText))
            {
                return string.Empty;
            }
            // Return string lower.
            return filterBoxText.ToLower();
        }
        static string UppercaseFirst(string filterBoxText)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(filterBoxText))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(filterBoxText[0]) + filterBoxText.Substring(1);
        }
        static string LowercaseFirst(string filterBoxText)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(filterBoxText))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToLower(filterBoxText[0]) + filterBoxText.Substring(1);
        }

        
    }
}
