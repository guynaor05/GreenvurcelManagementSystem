using GreenvurcelDAL;
using System;
using System.Collections.Generic;
using System.Net.Mail;
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
using WPFCustomMessageBox;

namespace GreenvurcelUI
{
    /// <summary>
    /// Interaction logic for RegistartionUpdateView.xaml
    /// </summary>
    public partial class UpdateDetailsView : UserControl
    {
        public static event Action<object> CustomerProductDeleted;

        private List<CustomerProduct> productsForCustomer;
        public UpdateDetailsView()
        {
            InitializeComponent();
            LoadCustomerProducts();
            BirthDate.DisplayDateEnd = DateTime.Now;
            
            MainWindow.CustomerUpdateRequest += MainWindow_CustomerUpdateRequest;

            CustomerProductsContext.Instance.ProdcutAdded += Instance_ProdcutAdded;

            CustomerContext.Instance.CustomerRemoved += Instance_CustomerRemoved;

            ProductsView.CustomerProductDeleted += UpdateDetailsView_CustomerProductDeleted;
        }

        private void Instance_CustomerRemoved(long id)
        {
            CancelChanges();
        }

        private void UpdateDetailsView_CustomerProductDeleted(object obj)
        {
            LoadCustomerProducts();
        }

        private void MainWindow_CustomerUpdateRequest(long obj)
        {
            CustomerID.Text = obj.ToString();
            LoadCustomer();
            LoadCustomerProducts();
        }

        

        private void AddPhone_Click(object sender, RoutedEventArgs e)
        {
            if (ValidatePhone(Phone.Text))
            { 
                Phone person = new Phone { PhoneType = ((ComboBoxItem)PhoneType.SelectedItem).Content.ToString(), PhoneNumber = Phone.Text };
                Phone.Text = "";
                Phones.Items.Add(person);
            }
            //if (ButtonEnable())
            //{
            //    UpadteDetailsButton.IsEnabled = true;
            //}
            //else
            //{
            //    UpadteDetailsButton.IsEnabled = false;

            //}
        }

        private void RemovePhone_Click(object sender, RoutedEventArgs e)
        {

            Button button = (Button)sender;
            Phone phoneToRemove = (Phone)button.DataContext;
            Phones.Items.Remove(phoneToRemove);
        }

        private void AddEmail_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateEmail(Email.Text))
            {
                Email person = new Email { EmailType = ((ComboBoxItem)EmailType.SelectedItem).Content.ToString(), EmailAddress = Email.Text };
                Email.Text = "";
                Emails.Items.Add(person);
            }
            
        }

        private void RemoveEmail_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Email EmailToRemove = (Email)button.DataContext;
            Emails.Items.Remove(EmailToRemove);
        }
       

        #region Validation Methods
        
        
        private bool ValidateEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }

        }

        private bool ValidatePhone(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            foreach (char c in text)
            {
                if (!char.IsDigit(c) && c != '-' && c != '(' && c != ')')
                {
                    return false;
                }
            }

            return true;
        }
        private bool ValidateNumbers(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            foreach (char c in text)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }


        
        #endregion


        private void UpadteDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            if (FirstName.Text == "" && LastName.Text == "" && HomeCountry.Text == "" && HomeCity.Text == "" && HomeStreet.Text == "" && Grade.Text == "" && WorkCountry.Text == ""
                && WorkCity.Text == "" && WorkStreet.Text == "" && HomePostalCode.Text == "" && WorkPostalCode.Text == "" && (Phones.Items.Count == 0 && Emails.Items.Count == 0 && BirthDate.SelectedDate == null && Notes.Text == "" && Job.Text == "" && WorkState.Text == "" && HomeState.Text == ""))
            {
                CustomMessageBox.Show("Cant register because all text boxes are empty");
            }

            else
            {
                List<Phone> phones = new List<Phone>();
                List<Email> emails = new List<Email>();
                foreach (Phone phone in Phones.Items)
                {
                    phones.Add(phone);
                }
                foreach (Email email in Emails.Items)
                {
                    emails.Add(email);
                }

                string customerID = CustomerID.Text;
                Customer customer = new Customer
                {
                    FirstName = FirstName.Text,
                    LastName = LastName.Text,
                    BirthDate = BirthDate.Text,
                    Grade = Grade.Text,
                    Job = Job.Text,
                    HomeState = HomeState.Text,
                    HomeCountry = HomeCountry.Text,
                    HomeCity = HomeCity.Text,
                    HomeStreet = HomeStreet.Text,
                    HomePostalCode = HomePostalCode.Text,
                    WorkPostalCode = WorkPostalCode.Text,
                    WorkState = WorkState.Text,
                    WorkCountry = WorkCountry.Text,
                    WorkCity = WorkCity.Text,
                    WorkStreet = WorkStreet.Text,
                    CompanyName = CompanyName.Text,
                    Phones = phones,
                    Emails = emails,
                    DefaultEmail = defaultEmailComboBox.Text,
                    Notes = Notes.Text
                };
                CustomerContext.Instance.UpdateCustomer(customerID, customer);
                CancelChanges();
                CustomMessageBox.Show("Updated Details Successfully");
            }
        }
    
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            CancelChanges();
        }
        private void ViewDetails_Click(object sender, RoutedEventArgs e)
        {
            bool isLoaded = LoadCustomer();
            if (isLoaded)
            {
                LoadCustomerProducts();
            }
        }
        private void CancelChanges()
        {
            IDPanel.Visibility = Visibility.Visible;
            ViewDetails.Visibility = Visibility.Visible;
            CustomerID.IsReadOnly = false;
            CustomerID.Text = "";
            FirstName.Text = "";
            LastName.Text = "";
            BirthDate.Text = "";
            Grade.Text = "";
            Job.Text = "";
            HomeState.Text = "";
            HomeCountry.Text = "";
            HomeCity.Text = "";
            HomeStreet.Text = "";
            HomePostalCode.Text = "";
            WorkPostalCode.Text = "";
            WorkState.Text = "";
            WorkCountry.Text = "";
            WorkCity.Text = "";
            WorkStreet.Text = "";
            CompanyName.Text = "";
            Phones.Items.Clear();
            Emails.Items.Clear();
            Notes.Text = "";
            ProductName.Text = "";
            Category.Text = "";
            FilterBox.Text = "";
            objectCheckBox.IsChecked = false;
            DetailsButtons.Visibility = Visibility.Collapsed;
            UpdateDetailsTabControl.Visibility = Visibility.Collapsed;
        }
        private bool LoadCustomer()
        {
            string customerID = CustomerID.Text;
            Customer details = CustomerContext.Instance.LoadCustomerById(customerID, out bool succeeded);
            if (ValidateNumbers(CustomerID.Text))
            {
                CustomerID.IsReadOnly = true;
                if (!succeeded)
                {
                    IfLoadCustomerFails();
                    return false;
                }
                if (details == null)
                {
                    IfLoadCustomerFails();
                    return false;
                }

                LoadCustomerDetails();
                return true;
            }

            CustomMessageBox.Show("Invalid input");
            return false;
            
        }
        private void IfLoadCustomerFails()
        {
            ViewDetails.Visibility = Visibility.Visible;
            IDPanel.Visibility = Visibility.Visible;
            CustomMessageBox.Show("Customer Id does not exist");
            CustomerID.IsReadOnly = false;
            DetailsButtons.Visibility = Visibility.Collapsed;
            UpdateDetailsTabControl.Visibility = Visibility.Collapsed;
        }
        private void LoadCustomerDetails()
        {
            string customerID = CustomerID.Text;
            Customer details = CustomerContext.Instance.LoadCustomerById(customerID, out bool succeeded);
            if (ValidateNumbers(CustomerID.Text))
            {
                ViewDetails.Visibility = Visibility.Collapsed;
                UpdateDetailsTabControl.Visibility = Visibility.Visible;
                DetailsButtons.Visibility = Visibility.Visible;
                FirstName.Text = details.FirstName;
                LastName.Text = details.LastName;
                BirthDate.Text = details.BirthDate;
                Grade.Text = details.Grade;
                Job.Text = details.Job;
                HomeState.Text = details.HomeState;
                HomeCountry.Text = details.HomeCountry;
                HomeCity.Text = details.HomeCity;
                HomeStreet.Text = details.HomeStreet;
                HomePostalCode.Text = details.HomePostalCode;
                WorkPostalCode.Text = details.WorkPostalCode;
                WorkState.Text = details.WorkState;
                WorkCountry.Text = details.WorkCountry;
                WorkCity.Text = details.WorkCity;
                WorkStreet.Text = details.WorkStreet;
                CompanyName.Text = details.CompanyName;
                defaultEmailComboBox.Text = details.DefaultEmail;
                Phones.Items.Clear();
                Emails.Items.Clear();
                Notes.Text = details.Notes;
                if (details.Phones != null)
                {
                    foreach (Phone phone in details.Phones)
                    {
                        Phones.Items.Add(phone);
                    }

                }

                if (details.Emails != null)
                {
                    foreach (Email email in details.Emails)
                    {
                        Emails.Items.Add(email);
                    }
                }
            }
        }
       
        private void Instance_CustomerRemoved()
        {
            LoadCustomerProducts();
            CancelChanges();
        }

        private void Instance_ProdcutAdded()
        {
            LoadCustomerProducts();
        }

        private void LoadCustomerProducts()
        {
            if(CustomerID.Text != "")
            {
                productsForCustomer = CustomerProductsContext.Instance.LoadCustomerProducts(CustomerID.Text);
                if (productsForCustomer == null)
                {
                    CustomMessageBox.Show("Unable to connect to databse");
                }
                else
                {
                    foreach (CustomerProduct product in productsForCustomer)
                    {
                        Customer CustomerDeitals = CustomerContext.Instance.LoadCustomerByIdForProduct(product.CustomerID, out bool succeeded);
                        product.FirstName = CustomerDeitals.FirstName;
                        product.LastName = CustomerDeitals.LastName;
                    }
                    Products.ItemsSource = productsForCustomer;
                    CustomerIDAdd.Text = CustomerID.Text;
                }
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

            if (selectedFilter == "Product")
            {
                if (FilterBox.Text != "")
                {
                    if (ValidateSpaceLetterAndNumber(FilterBox.Text))
                    {
                        string filterLower = Lowercase(FilterBox.Text);
                        List<CustomerProduct> filteredProducts = productsForCustomer.FindAll(product => product.ProductName != null && product.ProductName.ToLower().Contains(filterLower));
                        Products.ItemsSource = filteredProducts;
                    }
                    else
                    {
                        List<CustomerProduct> filteredProducts = productsForCustomer.FindAll(product => product.ProductName.Contains(FilterBox.Text));
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
                        List<CustomerProduct> filteredProducts = productsForCustomer.FindAll(product => product.CategoryName != null && product.CategoryName.ToLower().Contains(filterLower));
                        Products.ItemsSource = filteredProducts;
                    }
                    else
                    {
                        List<CustomerProduct> filteredProducts = productsForCustomer.FindAll(product => product.CategoryName.Contains(FilterBox.Text));
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
                        string filterLower = Lowercase(FilterBox.Text);
                        List<CustomerProduct> filteredCustomers = productsForCustomer.FindAll(product => product.IsObject.Equals(bool.Parse(filterLower)));
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
                    CustomerProductsContext.Instance.InsertCustomerProduct(CustomerProduct);
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

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CustomerProduct customerProduct = (CustomerProduct)Products.SelectedItem;
                if (customerProduct != null)
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
                            CustomerProductDeleted?.Invoke(customerProduct._id);
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
        
    }
}
