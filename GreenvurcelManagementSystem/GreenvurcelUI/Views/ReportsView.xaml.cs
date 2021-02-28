using ClosedXML.Excel;
using GreenvurcelDAL;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFCustomMessageBox;
using Outlook = Microsoft.Office.Interop.Outlook;
namespace GreenvurcelUI
{
    /// <summary>
    /// Interaction logic for ReportView.xaml
    /// </summary>
    public partial class ReportsView : UserControl
    {
        public Dictionary<long, string> CurrentSelectedEmail = new Dictionary<long, string>();
        private List<Customer> customers;
        public ICommand SwitchTabCommand { get; set; }

        public static event Action<long> CustomerUpdateRequest;
        public static event Action<long> ShowProdutsRequest;
        public static event Action<long> AddProductRequest;

        public ReportsView()
        {
            InitializeComponent();

            LoadCustomers();

            LoadCurrentSelectedEmail();

            CustomerContext.Instance.CustomerAdded += OnCustomerAdded;

            CustomerContext.Instance.CustomerUpadted += OnCustomerUpadted;
            //i need to find a way to do it when he opens the reports
        }

        private void OnCustomerAdded(long id, string email)
        {
            if(email != null)
            {
                CurrentSelectedEmail.Add(id, email);
            }

            LoadCustomers();
        }

        private void OnCustomerUpadted()
        {
            LoadCustomers();
        }
        private void LoadCurrentSelectedEmail()
        {
            foreach (Customer customer in customers)
            {
                if (customer.DefaultEmail != null)
                    CurrentSelectedEmail.Add(customer._id, customer.DefaultEmail);
            }
        }
        private void LoadCustomers()
        {
            customers = CustomerContext.Instance.LoadCustomers();
            if (customers == null)
            {
                CustomMessageBox.Show("Unable to connect to databse");
            }
            else
            {
                bool defaultEmailExist = false;
                string firstEmail = "";
                bool firstTime = true;
                foreach (Customer customer in customers)
                {
                    foreach (Email email in customer.Emails)
                    {
                        if (firstTime)
                        {
                            firstEmail = email.ToString();
                        }
                        if (customer.DefaultEmail != null)
                        {
                            defaultEmailExist = true;
                            break;
                        }
                        else
                        {
                            defaultEmailExist = false;
                        }
                    }
                    if (defaultEmailExist == false)
                    {
                        customer.DefaultEmail = firstEmail;
                    }
                }
                Customers.ItemsSource = customers;
            }
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedFilter = ((ComboBoxItem)FilterComboBox.SelectedItem).Content.ToString();
            if (selectedFilter == "First Name")
            {
                if (FilterBox.Text != "")
                {
                    if (ValidateSpaceLetterAndNumber(FilterBox.Text))
                    {
                        List<Customer> filteredCustomers = new List<Customer>();
                        string filterUpper = UppercaseFirst(FilterBox.Text);
                        List<Customer> filteredCustomersUpper = customers.FindAll(customer => customer.HomeState != null && customer.FirstName.Contains(filterUpper));
                        foreach (Customer filteredCustomerUpper in filteredCustomersUpper)
                        {
                            filteredCustomers.Add(filteredCustomerUpper);
                        }
                        string filterLower = LowercaseFirst(FilterBox.Text);
                        List<Customer> filteredCustomersLower = customers.FindAll(customer => customer.HomeState != null && customer.FirstName.Contains(filterUpper));
                        foreach (Customer filteredCustomerLower in filteredCustomersLower)
                        {
                            filteredCustomers.Add(filteredCustomerLower);
                        }
                        Customers.ItemsSource = filteredCustomers;
                    }
                    else
                    {
                        List<Customer> filteredCustomers = customers.FindAll(customer => customer.HomeState != null && customer.FirstName.Contains(FilterBox.Text));
                        Customers.ItemsSource = filteredCustomers;
                    }
                }
                else
                {
                    Customers.ItemsSource = null;
                }
            }
            else if (selectedFilter == "Last Name")
            {
                if (FilterBox.Text != "")
                {
                    if (ValidateSpaceLetterAndNumber(FilterBox.Text))
                    {
                        List<Customer> filteredCustomers = new List<Customer>();
                        string filterUpper = UppercaseFirst(FilterBox.Text);
                        List<Customer> filteredCustomersUpper = customers.FindAll(customer => customer.HomeState != null && customer.LastName.Contains(filterUpper));
                        foreach (Customer filteredCustomerUpper in filteredCustomersUpper)
                        {
                            filteredCustomers.Add(filteredCustomerUpper);
                        }
                        string filterLower = LowercaseFirst(FilterBox.Text);
                        List<Customer> filteredCustomersLower = customers.FindAll(customer => customer.HomeState != null && customer.LastName.Contains(filterUpper));
                        foreach (Customer filteredCustomerLower in filteredCustomersLower)
                        {
                            filteredCustomers.Add(filteredCustomerLower);
                        }
                        Customers.ItemsSource = filteredCustomers;
                        }
                    else
                    {
                        List<Customer> filteredCustomers = customers.FindAll(customer => customer.HomeState != null && customer.LastName.Contains(FilterBox.Text));
                        Customers.ItemsSource = filteredCustomers;
                    }
                }
                else
                {
                    Customers.ItemsSource = null;
                }
            }
            else if (selectedFilter == "Home Country")
            {
                if (FilterBox.Text != "")
                {
                    if (ValidateSpaceLetterAndNumber(FilterBox.Text))
                    {
                        List<Customer> filteredCustomers = new List<Customer>();
                        string filterUpper = UppercaseFirst(FilterBox.Text);
                        List<Customer> filteredCustomersUpper = customers.FindAll(customer => customer.HomeState != null && customer.HomeCountry.Contains(filterUpper));
                        foreach (Customer filteredCustomerUpper in filteredCustomersUpper)
                        {
                            filteredCustomers.Add(filteredCustomerUpper);
                        }
                        string filterLower = LowercaseFirst(FilterBox.Text);
                        List<Customer> filteredCustomersLower = customers.FindAll(customer => customer.HomeState != null && customer.HomeCountry.Contains(filterUpper));
                        foreach (Customer filteredCustomerLower in filteredCustomersLower)
                        {
                            filteredCustomers.Add(filteredCustomerLower);
                        }
                        Customers.ItemsSource = filteredCustomers;
                    }
                    else
                    {
                        List<Customer> filteredCustomers = customers.FindAll(customer => customer.HomeState != null && customer.HomeCountry.Contains(FilterBox.Text));
                        Customers.ItemsSource = filteredCustomers;
                    }
                }
                else
                {
                    Customers.ItemsSource = null;
                }
            }
            
            else if (selectedFilter == "Home State")
            {
                if (FilterBox.Text != "")
                {
                    if (ValidateSpaceLetterAndNumber(FilterBox.Text))
                    {
                        List<Customer> filteredCustomers = new List<Customer>();
                        string filterUpper = UppercaseFirst(FilterBox.Text);
                        List<Customer> filteredCustomersUpper = customers.FindAll(customer => customer.HomeState != null && customer.HomeState.Contains(filterUpper));
                        foreach (Customer filteredCustomerUpper in filteredCustomersUpper)
                        {
                            filteredCustomers.Add(filteredCustomerUpper);
                        }
                        string filterLower = LowercaseFirst(FilterBox.Text);
                        List<Customer> filteredCustomersLower = customers.FindAll(customer => customer.HomeState != null && customer.HomeState.Contains(filterLower));
                        foreach (Customer filteredCustomerLower in filteredCustomersLower)
                        {
                            filteredCustomers.Add(filteredCustomerLower);
                        }
                        Customers.ItemsSource = filteredCustomers;
                    }
                    else
                    {
                        List<Customer> filteredCustomers = customers.FindAll(customer => customer.HomeState != null && customer.HomeState.Contains(FilterBox.Text));
                        Customers.ItemsSource = filteredCustomers;
                    }
                }
                else
                {
                    Customers.ItemsSource = null;
                }
            }
            else if (selectedFilter == "Grade")
            {
                if (FilterBox.Text != "")
                {
                    if (ValidateNumber(FilterBox.Text))
                    {
                        List<Customer> filteredCustomers = customers.FindAll(customer => customer.Grade != null && customer.Grade == FilterBox.Text);
                        Customers.ItemsSource = filteredCustomers;
                    }
                    else
                    {
                        Customers.ItemsSource = null;
                    }
                }
                else
                {
                    Customers.ItemsSource = null;
                }
            }
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
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
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
            if (CustomMessageBox.Show($"Are you sure you want to delete customer - {customerDetails._id}?", "Delete Customer", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                bool succeeded = CustomerContext.Instance.DeleteCustomer(customerDetails._id);
                if (!succeeded)
                {
                    CustomMessageBox.Show("Unable to connect to databse");
                }
                else
                {
                    LoadCustomers();
                    CurrentSelectedEmail.Remove(customerDetails._id);
                    CustomMessageBox.Show("Customer deleted Successfully");
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
            if(Customers.ItemsSource != null)
            {
                ListToExcel(Customers.ItemsSource.Cast<Customer>().ToList());
            }
            else
            {
                CustomMessageBox.Show("Cant export without customers");
            }
        }

        private void ListToExcel(List<Customer> list)
        {
            var workbook = new XLWorkbook();
            workbook.AddWorksheet("Customers");
            MakeSheet(workbook, list);
            SaveFileDialog(workbook);
        }
        private void MakeSheet(IXLWorkbook workbook, List<Customer> list)
        {
            var sheet = workbook.Worksheet("Customers");
            sheet.Cell("A1").Value = "Customer ID";
            sheet.Cell("B1").Value = "First Name";
            sheet.Cell("C1").Value = "Last Name";
            sheet.Cell("D1").Value = "Birth Date";
            sheet.Cell("E1").Value = "Grade";
            sheet.Cell("F1").Value = "Home Country";
            sheet.Cell("G1").Value = "Home City";
            sheet.Cell("H1").Value = "Home Street";
            sheet.Cell("I1").Value = "Home Postal Code";
            sheet.Cell("J1").Value = "Work Postal Code";
            sheet.Cell("K1").Value = "Work Country";
            sheet.Cell("L1").Value = "Work City";
            sheet.Cell("M1").Value = "Work Street";
            sheet.Cell("N1").Value = "Company Name";
            sheet.Cell("O1").Value = "Home State";
            sheet.Cell("P1").Value = "Work State";
            sheet.Cell("Q1").Value = "Job";
            sheet.Cell("R1").Value = "Home Phone";
            sheet.Cell("S1").Value = "Work Phone";
            sheet.Cell("T1").Value = "Mobile Phone";
            sheet.Cell("U1").Value = "Fax";
            sheet.Cell("V1").Value = "Personal Email";
            sheet.Cell("W1").Value = "Work Email";
            sheet.Cell("X1").Value = "Notes";

            int counter = 2;
            foreach (Customer item in list)
            {
                sheet.Cell("A" + counter.ToString()).Value = item._id;
                sheet.Cell("B" + counter.ToString()).Value = item.FirstName;
                sheet.Cell("C" + counter.ToString()).Value = item.LastName;
                sheet.Cell("D" + counter.ToString()).Value = item.BirthDate;
                sheet.Cell("E" + counter.ToString()).Value = item.Grade;
                sheet.Cell("F" + counter.ToString()).Value = item.HomeCountry;
                sheet.Cell("G" + counter.ToString()).Value = item.HomeCity;
                sheet.Cell("H" + counter.ToString()).Value = item.HomeStreet;
                sheet.Cell("I" + counter.ToString()).Value = item.HomePostalCode;
                sheet.Cell("J" + counter.ToString()).Value = item.WorkPostalCode;
                sheet.Cell("K" + counter.ToString()).Value = item.WorkCountry;
                sheet.Cell("L" + counter.ToString()).Value = item.WorkCity;
                sheet.Cell("M" + counter.ToString()).Value = item.WorkStreet;
                sheet.Cell("N" + counter.ToString()).Value = item.CompanyName;
                sheet.Cell("O" + counter.ToString()).Value = item.HomeState;
                sheet.Cell("P" + counter.ToString()).Value = item.WorkState;
                sheet.Cell("Q" + counter.ToString()).Value = item.Job;
                sheet.Cell("X" + counter.ToString()).Value = item.Notes;
                foreach (Phone phone in item.Phones)
                {
                    if (sheet.Cell("R" + counter.ToString()).Value != null && phone.PhoneType == "Home")
                    {
                        sheet.Cell("R" + counter.ToString()).Value = phone.PhoneNumber;


                    }
                    if (sheet.Cell("S" + counter.ToString()).Value != null && phone.PhoneType == "Work")
                    {
                        sheet.Cell("S" + counter.ToString()).Value = phone.PhoneNumber;


                    }
                    if (sheet.Cell("T" + counter.ToString()).Value != null && phone.PhoneType == "Mobile")
                    {
                        sheet.Cell("T" + counter.ToString()).Value = phone.PhoneNumber;


                    };
                    if (sheet.Cell("U" + counter.ToString()).Value != null && phone.PhoneType == "Fax")
                    {
                        sheet.Cell("U" + counter.ToString()).Value = phone.PhoneNumber;


                    };
                }
                foreach (Email email in item.Emails)
                {
                    if (sheet.Cell("V" + counter.ToString()).Value != null && email.EmailType == "Personal")
                    {
                        sheet.Cell("V" + counter.ToString()).Value = email.EmailAddress;


                    }
                    if (sheet.Cell("W" + counter.ToString()).Value != null && email.EmailType == "Work")
                    {
                        sheet.Cell("W" + counter.ToString()).Value = email.EmailAddress;


                    };

                }
                counter++;
            }
        }
        private void SaveFileDialog(IXLWorkbook workbook)
        {
            SaveFileDialog dlg = new SaveFileDialog
            {
                FileName = "Customers", // Default file name
                DefaultExt = ".xlsx", // Default file extension
                Filter = "Execl files (*.xlsx)|*.xlsx" // Filter files by extension
            };

            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;

                workbook.SaveAs(Path.GetFullPath(dlg.FileName.ToString()));
            }
        }

        private void OutLook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string toGroup = "";
                if (Customers.SelectedItems.Cast<Customer>().ToList().Count == 0)
                {
                    toGroup = GetAllViewedCustomersEmails();
                }
                else
                {
                    string SelectedEmails = GetSelectedCustomersEmails();
                    if (SelectedEmails == "")
                    {
                        MessageBoxResult mbResultForNoEmails = CustomMessageBox.ShowOKCancel("No emails in selected customers do you want to send email to all viewed customers?",
                                                                              "Send Email",
                                                                              "Yes",
                                                                              "No");
                        switch (mbResultForNoEmails) 
                        {
                            case MessageBoxResult.OK:
                                toGroup = GetAllViewedCustomersEmails();
                                StartOutLook(toGroup);
                                return;

                            case MessageBoxResult.Cancel:
                                return;

                            case MessageBoxResult.None:
                                return;
                        }
                        
                    }
                    MessageBoxResult mbResult = CustomMessageBox.ShowOKCancel("Send email to selected customers or to all viewed customers",
                                                                              "Send Email",
                                                                              "All viewed customers",
                                                                              "Selected customers");
                    switch (mbResult)
                    {
                        case MessageBoxResult.OK:
                            toGroup = GetAllViewedCustomersEmails();
                            break;

                        case MessageBoxResult.Cancel:
                            toGroup = GetSelectedCustomersEmails();
                            break;

                        case MessageBoxResult.None:
                            return;
                    }
                }

                StartOutLook(toGroup);
            }
            catch (Exception)
            {
                CustomMessageBox.Show("Error");
            }
        }

        private void StartOutLook(string toGroup)
        {
            var startInfo = new ProcessStartInfo()
            {
                FileName = "Outlook.exe",
                UseShellExecute = true
            };
            Process.Start(startInfo);

            Outlook.Application oApp = new Outlook.Application();
            Outlook.MailItem mailItem = (Outlook.MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);
            Outlook.Inspector oInspector = mailItem.GetInspector;
            var insp = mailItem.GetInspector;
            mailItem.Subject = "";
            mailItem.To = toGroup;
            mailItem.Body = "";
            mailItem.Display(true);
        }

        private string GetSelectedCustomersEmails()
        {
            string toGroup = "";
            foreach (Customer customer in Customers.SelectedItems.Cast<Customer>().ToList())
            {
                long id = customer._id;
                if (CurrentSelectedEmail.TryGetValue(id, out string defaultEmail))
                {
                    toGroup += defaultEmail + ";";
                }
            }
            return toGroup;
        }

        private string GetAllViewedCustomersEmails()
        {
            string toGroup = "";
            foreach (Customer customer in Customers.ItemsSource.Cast<Customer>().ToList())
            {
                long id = customer._id;
                string DefaultEmail;
                if (CurrentSelectedEmail.TryGetValue(id, out DefaultEmail))
                {
                    toGroup += DefaultEmail + ";";
                }
            }
            return toGroup;
        }

        private void EmailsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Customer customerDataContext = (Customer)comboBox.DataContext;
            Email customerSelectedItem = (Email)comboBox.SelectedItem;
            if(customerSelectedItem != null)
            {
                string newdefaultEmail = customerSelectedItem.EmailAddress;
                long id = customerDataContext._id;
                string oldDefaultEmail;
                if (CurrentSelectedEmail.TryGetValue(id, out oldDefaultEmail))
                {
                    CurrentSelectedEmail[id] = newdefaultEmail;
                }
            }
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

