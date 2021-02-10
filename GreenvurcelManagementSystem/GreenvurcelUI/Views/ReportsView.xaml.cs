using GreenvurcelDAL;
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
using System.IO;
using System.Diagnostics;

namespace GreenvurcelUI
{
    /// <summary>
    /// Interaction logic for ReportView.xaml
    /// </summary>
    public partial class ReportsView : UserControl
    {
        private List<Customer> customers;
        public ICommand SwitchTabCommand { get; set; }

        public static event Action<long> CustomerUpdateRequest;
        public static event Action<long> ShowProdutsRequest;
        public static event Action<long> AddProductRequest;

        public ReportsView()
        {
            InitializeComponent();

            LoadCustomers();

            CustomerContext.Instance.CustomerAdded += OnCustomerAdded;

            CustomerContext.Instance.CustomerUpadted += Instance_CustomerUpadted;
            //i need to find a way to do it when he opens the reports
        }

        private void Instance_CustomerUpadted()
        {
            LoadCustomers();
        }

        private void OnCustomerAdded()
        {
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            customers = CustomerContext.Instance.LoadCustomers();
            if (customers == null)
            {
                MessageBox.Show("Unable to connect to databse");
            }
            else
            {
                Customers.ItemsSource = customers; 
            }
        }
        
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedFilter = ((ComboBoxItem)FilterComboBox.SelectedItem).Content.ToString();
            if (selectedFilter == "First Name")
            {
                List<Customer> filteredCustomers = customers.FindAll(customer => customer.FirstName == FilterBox.Text);
                Customers.ItemsSource = filteredCustomers;
            }
            else if (selectedFilter == "Last Name")
            {
                List<Customer> filteredCustomers = customers.FindAll(customer => customer.LastName == FilterBox.Text);
                Customers.ItemsSource = filteredCustomers;
            }
            else if (selectedFilter == "Home Country")
            {
                List<Customer> filteredCustomers = customers.FindAll(customer => customer.HomeCountry == FilterBox.Text);
                Customers.ItemsSource = filteredCustomers;
            }
            else if (selectedFilter == "Home State")
            {
                List<Customer> filteredCustomers = customers.FindAll(customer => customer.HomeState == FilterBox.Text);
                Customers.ItemsSource = filteredCustomers;
            }
            else if (selectedFilter == "Grade")
            {
                List<Customer> filteredCustomers = customers.FindAll(customer => customer.Grade == FilterBox.Text);
                Customers.ItemsSource = filteredCustomers;
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine(Customers.SelectedItems);
            LoadCustomers();
        }
        private void UpdateCustomer(object sender, RoutedEventArgs e)
        {
            Customer customerDetails = (Customer)Customers.SelectedItem;
            CustomerUpdateRequest?.Invoke(customerDetails._id);
        }

        private void DeleteCustomer(object sender, RoutedEventArgs e)
        {
            Customer customerDetails = (Customer)Customers.SelectedItem;

            if (MessageBox.Show($"Are you sure you want to delete customer - {customerDetails._id}?", "Delete Customer", MessageBoxButton.OKCancel) != MessageBoxResult.Cancel)
            {
                bool succeeded = CustomerContext.Instance.DeleteCustomer(customerDetails._id);
                if (!succeeded)
                {
                    MessageBox.Show("Unable to connect to databse");
                }
                else
                {
                    LoadCustomers();
                    MessageBox.Show("Customer deleted Successfully");
                } 
            }
        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {
            Customer customerDetails = (Customer)Customers.SelectedItem;
            AddProductRequest?.Invoke(customerDetails._id);
        }

        private void ShowProduct(object sender, RoutedEventArgs e)
        {
            Customer customerDetails = (Customer)Customers.SelectedItem;
            ShowProdutsRequest?.Invoke(customerDetails._id);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Customers.SelectAllCells();
            //Customers.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            //try
            //{
            //    ApplicationCommands.Copy.Execute(null, Customers);
            //    Customers.UnselectAllCells();
            //    String Clipboardresult = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            //    StreamWriter swObj = new StreamWriter("exportToExcelTest.csv");
            //    swObj.WriteLine(Clipboardresult);
            //    swObj.Close();
            //    Process.Start(@"cmd.exe ",@"/c C:\Users\user2\Desktop\XXXX.reg");

            //    MessageBox.Show(" Exporting DataGrid data to Excel file created.xls");
            //}
            //catch
            //{
            //}
            
        }
    }
}

