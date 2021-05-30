using GreenvurcelDAL;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : UserControl
    {
        public RegistrationView()
        {
            InitializeComponent();
            BirthDate.DisplayDateEnd = DateTime.Now;
        }
        

        private void AddPhone_Click(object sender, RoutedEventArgs e)
        {

            if (ValidatePhone(Phone.Text))
            {
                Phone person = new Phone { PhoneType = ((ComboBoxItem)PhoneType.SelectedItem).Content.ToString(), PhoneNumber = Phone.Text };
                Phone.Text = "";
                Phones.Items.Add(person);
            }
            
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
        private bool ValidateNotes(string text)
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
        private bool ValidateNames(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            foreach (char c in text)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }

            return true;
        }

        private bool ValidateContryAndCity(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            foreach (char c in text)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                {
                    return false;
                }
            }

            return true;
        }

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


        private bool ValidateStreet(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            foreach (char c in text)
            {
                if (!char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
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

                Customer customer = new Customer
                {
                    FirstName = FirstName.Text,
                    LastName = LastName.Text,
                    BirthDate = BirthDate.Text,
                    Grade = Grade.Text,
                    Job = Job.Text,
                    HomeCountry = HomeCountry.Text,
                    HomeCity = HomeCity.Text,
                    HomeStreet = HomeStreet.Text,
                    HomePostalCode = HomePostalCode.Text,
                    WorkPostalCode = WorkPostalCode.Text,
                    WorkState = WorkState.Text,
                    HomeState = HomeState.Text,
                    WorkCountry = WorkCountry.Text,
                    WorkCity = WorkCity.Text,
                    WorkStreet = WorkStreet.Text,
                    CompanyName = CompanyName.Text,
                    DefaultEmail = defaultEmailComboBox.Text,
                    Phones = phones,
                    Emails = emails,
                    Notes = Notes.Text
                };
                CustomerContext.Instance.InsertCustomer(customer);
                FirstName.Text = "";
                LastName.Text = "";
                BirthDate.Text = "";
                Grade.Text = "";
                Job.Text = "";
                HomeCountry.Text = "";
                HomeCity.Text = "";
                HomeStreet.Text = "";
                HomePostalCode.Text = "";
                WorkCountry.Text = "";
                WorkCity.Text = "";
                HomeState.Text = "";
                WorkState.Text = "";
                WorkPostalCode.Text = "";
                WorkStreet.Text = "";
                CompanyName.Text = "";
                Phones.Items.Clear();
                Emails.Items.Clear();
                Notes.Text = "";
                CustomMessageBox.Show("Registered Successfully");            
            }
        }
    }

}
