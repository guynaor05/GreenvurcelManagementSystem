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

namespace GreenvurcelUI
{
    /// <summary>
    /// Interaction logic for ReportView.xaml
    /// </summary>
    public partial class ReportsView : UserControl
    {
        private List<Customer> customers;

        public ReportsView()
        {
            InitializeComponent();

            LoadCustomers();
        }


        private void LoadCustomers()
        {
            customers = new List<Customer>
            {
                new Customer
                {
                    PostalCode = "13412",
                    CompanyName = "Compnay",
                    FirstName = "Guy",
                    LastName = "naor",
                    BirthDate = "12/12/2007",
                    HomeCountry = "hod",
                    HomeCity = "hod",
                    HomeStreet = "hod",
                    WorkCountry = "hod",
                    WorkCity = "hod",
                    WorkStreet = "hod",
                    Grade = "10",
                    Phones = new List<Phone> { new Phone { PhoneType = "Mobile", PhoneNumber = "0"}, new Phone { PhoneType = "Home", PhoneNumber = "1" }, new Phone { PhoneType = "Mobile", PhoneNumber = "2" } },
                    Emails = new List<Email> { new Email { EmailType = "Work", EmailAddress = "0"}, new Email { EmailType = "Personal", EmailAddress = "1" } }
                    
                },
                new Customer
                {
                    PostalCode = "13412",
                    CompanyName = "Compnay",
                    FirstName = "Sean",
                    LastName = "naor",
                    BirthDate = "12/12/2008",
                    HomeCountry = "hod",
                    HomeCity = "hod",
                    HomeStreet = "hod",
                    WorkCountry = "hod",
                    WorkCity = "hod",
                    WorkStreet = "hod",
                    Grade = "3",
                    Phones = new List<Phone> { new Phone { PhoneType = "Mobile", PhoneNumber = "0"}, new Phone { PhoneType = "Home", PhoneNumber = "1" }, new Phone { PhoneType = "Mobile", PhoneNumber = "2" } },
                    Emails = new List<Email> { new Email { EmailType = "Work", EmailAddress = "0"}, new Email { EmailType = "Personal", EmailAddress = "1" } }
                },
                new Customer
                {
                    PostalCode = "13412",
                    CompanyName = "Compnay",
                    FirstName = "Ron",
                    LastName = "naor",
                    BirthDate = "12/11/2008",
                    HomeCountry = "hod",
                    HomeCity = "hod",
                    HomeStreet = "hod",
                    WorkCountry = "hod",
                    WorkCity = "hod",
                    WorkStreet = "hod",
                    Grade = "3",
                    Phones = new List<Phone> { new Phone { PhoneType = "Mobile", PhoneNumber = "0"}, new Phone { PhoneType = "Home", PhoneNumber = "1" }, new Phone { PhoneType = "Mobile", PhoneNumber = "2" } },
                    Emails = new List<Email> { new Email { EmailType = "Work", EmailAddress = "0"}, new Email { EmailType = "Personal", EmailAddress = "1" } }
                },
                new Customer
                {
                    PostalCode = "13412",
                    CompanyName = "Compnay",
                    FirstName = "Dana",
                    LastName = "naor",
                    BirthDate = "12/12/2006",
                    HomeCountry = "hod",
                    HomeCity = "hod",
                    HomeStreet = "hod",
                    WorkCountry = "hod",
                    WorkCity = "hod",
                    WorkStreet = "hod",
                    Grade = "1",
                    Phones = new List<Phone> { new Phone { PhoneType = "Mobile", PhoneNumber = "0"}, new Phone { PhoneType = "Home", PhoneNumber = "1" }, new Phone { PhoneType = "Mobile", PhoneNumber = "2" } },
                    Emails = new List<Email> { new Email { EmailType = "Work", EmailAddress = "0"}, new Email { EmailType = "Personal", EmailAddress = "1" } }
                },
                new Customer
                {
                    PostalCode = "13412",
                    CompanyName = "Compnay",
                    FirstName = "Uri",
                    LastName = "naor",
                    BirthDate = "12/12/2006",
                    HomeCountry = "hod",
                    HomeCity = "hod",
                    HomeStreet = "hod",
                    WorkCountry = "hod",
                    WorkCity = "hod",
                    WorkStreet = "hod",
                    Grade = "2",
                    Phones = new List<Phone> { new Phone { PhoneType = "Mobile", PhoneNumber = "0"}, new Phone { PhoneType = "Home", PhoneNumber = "1" }, new Phone { PhoneType = "Mobile", PhoneNumber = "2" } },
                    Emails = new List<Email> { new Email { EmailType = "Work", EmailAddress = "0"}, new Email { EmailType = "Personal", EmailAddress = "1" } }
                },
                new Customer
                {
                    PostalCode = "13412",
                    CompanyName = "Compnay",
                    FirstName = "Guy",
                    LastName = "naor",
                    BirthDate = "12/12/2006",
                    HomeCountry = "hod",
                    HomeCity = "hod",
                    HomeStreet = "hod",
                    WorkCountry = "hod",
                    WorkCity = "hod",
                    WorkStreet = "hod",
                    Grade = "10",
                    Phones = new List<Phone> { new Phone { PhoneType = "Mobile", PhoneNumber = "0"}, new Phone { PhoneType = "Home", PhoneNumber = "1" }, new Phone { PhoneType = "Mobile", PhoneNumber = "2" } },
                    Emails = new List<Email> { new Email { EmailType = "Work", EmailAddress = "0"}, new Email { EmailType = "Personal", EmailAddress = "1" } }
                },
                new Customer
                {
                    PostalCode = "13412",
                    CompanyName = "Compnay",
                    FirstName = "Guy",
                    LastName = "naor",
                    BirthDate = "12/12/2006",
                    HomeCountry = "hod",
                    HomeCity = "hod",
                    HomeStreet = "hod",
                    WorkCountry = "hod",
                    WorkCity = "hod",
                    WorkStreet = "hod",
                    Grade = "2",
                    Phones = new List<Phone> { new Phone { PhoneType = "Mobile", PhoneNumber = "0"}, new Phone { PhoneType = "Home", PhoneNumber = "1" }, new Phone { PhoneType = "Mobile", PhoneNumber = "2" } },
                    Emails = new List<Email> { new Email { EmailType = "Work", EmailAddress = "0"}, new Email { EmailType = "Personal", EmailAddress = "1" } }
                },
                new Customer
                {
                    PostalCode = "13412",
                    CompanyName = "Compnay",
                    FirstName = "Guy",
                    LastName = "naor",
                    BirthDate = "12/12/2006",
                    HomeCountry = "hod",
                    HomeCity = "hod",
                    HomeStreet = "hod",
                    WorkCountry = "hod",
                    WorkCity = "hod",
                    WorkStreet = "hod",
                    Grade = "4",
                    Phones = new List<Phone> { new Phone { PhoneType = "Mobile", PhoneNumber = "0"}, new Phone { PhoneType = "Home", PhoneNumber = "1" }, new Phone { PhoneType = "Mobile", PhoneNumber = "2" } },
                    Emails = new List<Email> { new Email { EmailType = "Work", EmailAddress = "0"}, new Email { EmailType = "Personal", EmailAddress = "1" } }
                },
            };

            Customers.ItemsSource = customers;
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
            else if (selectedFilter == "Home City")
            {
                List<Customer> filteredCustomers = customers.FindAll(customer => customer.HomeCity == FilterBox.Text);
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
            LoadCustomers();
        }
    }
}

