﻿using GreenvurcelDAL;
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
            Personal.IsExpanded = true;
        }
        public bool ButtonEnable()
        {
            if (!ValidateNames(FirstName.Text) || !ValidateNames(LastName.Text) || !ValidateContryAndCity(HomeCountry.Text) || !ValidateContryAndCity(HomeCity.Text) || !ValidateStreet(HomeStreet.Text) || !ValidatePhone(Grade.Text) || !ValidateContryAndCity(WorkCountry.Text)
                || !ValidateContryAndCity(WorkCity.Text) || !ValidateStreet(WorkStreet.Text) || !ValidatePhone(PostalCode.Text) || (Phones.Items.Count == 0 || Emails.Items.Count == 0 || BirthDate.SelectedDate == null|| !ValidateNotes(Notes.Text) || BirthDate.SelectedDate > DateTime.Now))
            {
                return false;

            }
            return true;
        }

        private void AddPhone_Click(object sender, RoutedEventArgs e)
        {

            if (ValidatePhone(Phone.Text))
            {
                Phone person = new Phone { PhoneType = ((ComboBoxItem)PhoneType.SelectedItem).Content.ToString(), PhoneNumber = Phone.Text };
                Phone.Text = "";
                Phone.BorderBrush = Brushes.Red;
                Phones.Items.Add(person);
            }
            if (ButtonEnable())
            {
                SubmitButton.IsEnabled = true;
            }
            else
            {
                SubmitButton.IsEnabled = false;

            }
        }

        private void RemovePhone_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Phone phoneToRemove = (Phone)button.DataContext;
            Phones.Items.Remove(phoneToRemove);
            Console.WriteLine();
            if (Phones.Items.Count == 0)
            {
                SubmitButton.IsEnabled = false;

            }
        }

        private void AddEmail_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateEmail(Email.Text))
            {
                Email person = new Email { EmailType = ((ComboBoxItem)EmailType.SelectedItem).Content.ToString(), EmailAddress = Email.Text };
                Email.Text = "";
                Email.BorderBrush = Brushes.Red;
                Emails.Items.Add(person);
            }
            if (ButtonEnable())
            {
                SubmitButton.IsEnabled = true;
            }
            else
            {
                SubmitButton.IsEnabled = false;

            }
        }

        private void RemoveEmail_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Email EmailToRemove = (Email)button.DataContext;
            Emails.Items.Remove(EmailToRemove);
            Console.WriteLine();
            if (Emails.Items.Count == 0)
            {
                SubmitButton.IsEnabled = false;

            }
        }
        
        private void FirstName_TextChanged(object sender, EventArgs e)
        {

            TextBox textBox = (TextBox)sender;
            if (ButtonEnable())
            {
                SubmitButton.IsEnabled = true;
            }
            else
            {
                SubmitButton.IsEnabled = false;

            }
            if (!ValidateNames(textBox.Text))
            {
                FirstName.BorderBrush = Brushes.Red;
            }
            else
            {
                FirstName.BorderBrush = Brushes.Black;
            }
        }

        private void LastName_TextChanged(object sender, EventArgs e)
        {

            TextBox textBox = (TextBox)sender;
            if (ButtonEnable())
            {
                SubmitButton.IsEnabled = true;
            }
            else
            {
                SubmitButton.IsEnabled = false;

            }
            if (!ValidateNames(textBox.Text))
            {
                LastName.BorderBrush = Brushes.Red;
            }
            else
            {
                LastName.BorderBrush = Brushes.Black;
            }
        }


        private void BirthDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = (DatePicker)sender;
            DateTime? selectedDate = datePicker.SelectedDate;
            if (ButtonEnable())
            {
                SubmitButton.IsEnabled = true;
            }
            else
            {
                SubmitButton.IsEnabled = false;

            }
            if (selectedDate == null || selectedDate > DateTime.Now)
            {
                BirthDate.BorderBrush = Brushes.Red;
            }
            else
            {
                BirthDate.BorderBrush = Brushes.Black;
            }
        }
        private void Grade_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (ButtonEnable())
            {
                SubmitButton.IsEnabled = true;
            }
            else
            {
                SubmitButton.IsEnabled = false;

            }
            if (!ValidatePhone(textBox.Text))
            {
                Grade.BorderBrush = Brushes.Red;
            }
            else
            {
                Grade.BorderBrush = Brushes.Black;
            }
        }

        private void HomeCountry_TextChanged(object sender, EventArgs e)
        {

            TextBox textBox = (TextBox)sender;
            if (ButtonEnable())
            {
                SubmitButton.IsEnabled = true;
            }
            else
            {
                SubmitButton.IsEnabled = false;

            }
            if (!ValidateContryAndCity(textBox.Text))
            {
                HomeCountry.BorderBrush = Brushes.Red;
            }
            else
            {
                HomeCountry.BorderBrush = Brushes.Black;
            }
        }

        private void HomeCity_TextChanged(object sender, EventArgs e)
        {

            TextBox textBox = (TextBox)sender;
            if (ButtonEnable())
            {
                SubmitButton.IsEnabled = true;
            }
            else
            {
                SubmitButton.IsEnabled = false;

            }
            if (!ValidateContryAndCity(textBox.Text))
            {
                HomeCity.BorderBrush = Brushes.Red;
            }
            else
            {
                HomeCity.BorderBrush = Brushes.Black;
            }
        }
        private void HomeStreet_TextChanged(object sender, EventArgs e)
        {

            TextBox textBox = (TextBox)sender;
            if (ButtonEnable())
            {
                SubmitButton.IsEnabled = true;
            }
            else
            {
                SubmitButton.IsEnabled = false;

            }
            if (!ValidateStreet(textBox.Text))
            {
                HomeStreet.BorderBrush = Brushes.Red;
            }
            else
            {
                HomeStreet.BorderBrush = Brushes.Black;
            }
        }
        private void PostalCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (ButtonEnable())
            {
                SubmitButton.IsEnabled = true;
            }
            else
            {
                SubmitButton.IsEnabled = false;

            }
            if (!ValidatePhone(textBox.Text))
            {
                PostalCode.BorderBrush = Brushes.Red;
            }
            else
            {
                PostalCode.BorderBrush = Brushes.Black;
            }
        }
        private void WorkCountry_TextChanged(object sender, EventArgs e)
        {

            TextBox textBox = (TextBox)sender;
            if (ButtonEnable())
            {
                SubmitButton.IsEnabled = true;
            }
            else
            {
                SubmitButton.IsEnabled = false;

            }
            if (!ValidateContryAndCity(textBox.Text))
            {
                WorkCountry.BorderBrush = Brushes.Red;
            }
            else
            {
                WorkCountry.BorderBrush = Brushes.Black;
            }
        }


        private void WorkCity_TextChanged(object sender, EventArgs e)
        {

            TextBox textBox = (TextBox)sender;
            if (ButtonEnable())
            {
                SubmitButton.IsEnabled = true;
            }
            else
            {
                SubmitButton.IsEnabled = false;

            }
            if (!ValidateContryAndCity(textBox.Text))
            {
                WorkCity.BorderBrush = Brushes.Red;
            }
            else
            {
                WorkCity.BorderBrush = Brushes.Black;
            }
        }

        private void WorkStreet_TextChanged(object sender, EventArgs e)
        {

            TextBox textBox = (TextBox)sender;
            if (ButtonEnable())
            {
                SubmitButton.IsEnabled = true;
            }
            else
            {
                SubmitButton.IsEnabled = false;

            }
            if (!ValidateStreet(textBox.Text))
            {
                WorkStreet.BorderBrush = Brushes.Red;
            }
            else
            {
                WorkStreet.BorderBrush = Brushes.Black;
            }
        }
        private void CompanyName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (ButtonEnable())
            {
                SubmitButton.IsEnabled = true;
            }
            else
            {
                SubmitButton.IsEnabled = false;

            }
            if (!ValidateContryAndCity(textBox.Text))
            {
                CompanyName.BorderBrush = Brushes.Red;
            }
            else
            {
                CompanyName.BorderBrush = Brushes.Black;
            }
        }

        private void Phone_TextChanged(object sender, EventArgs e)
        {

            TextBox textBox = (TextBox)sender;
            if (ButtonEnable())
            {
                SubmitButton.IsEnabled = true;
            }
            else
            {
                SubmitButton.IsEnabled = false;

            }
            if (!ValidatePhone(textBox.Text))
            {
                Phone.BorderBrush = Brushes.Red;
            }
            else
            {
                Phone.BorderBrush = Brushes.Black;
            }
        }

        private void Email_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (ButtonEnable())
            {
                SubmitButton.IsEnabled = true;
            }
            else
            {
                SubmitButton.IsEnabled = false;

            }
            if (!ValidateEmail(textBox.Text))
            {
                Email.BorderBrush = Brushes.Red;
            }
            else
            {
                Email.BorderBrush = Brushes.Black;
            }
        }
        private void Notes_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (ButtonEnable())
            {
                SubmitButton.IsEnabled = true;
            }
            else
            {
                SubmitButton.IsEnabled = false;

            }
            if (!ValidateNotes(textBox.Text))
            {
                Notes.BorderBrush = Brushes.Red;
            }
            else
            {
                Notes.BorderBrush = Brushes.Black;
            }
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
                if (!char.IsDigit(c))
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
                HomeCountry = HomeCountry.Text,
                HomeCity = HomeCity.Text,
                HomeStreet = HomeStreet.Text,
                PostalCode = PostalCode.Text,
                WorkCountry = WorkCountry.Text,
                WorkCity = WorkCity.Text,
                WorkStreet = WorkStreet.Text,
                CompanyName = CompanyName.Text,
                Phones = phones,
                Emails = emails,
                Notes = Notes.Text
            };
            CustomerContext.Instance.InsertCustomer(customer);
            FirstName.Text = "";
            LastName.Text = "";
            BirthDate.Text = "";
            Grade.Text = "";
            HomeCountry.Text = "";
            HomeCity.Text = "";
            HomeStreet.Text = "";
            PostalCode.Text = "";
            WorkCountry.Text = "";
            WorkCity.Text = "";
            WorkStreet.Text = "";
            CompanyName.Text = "";
            Phones.Items.Clear();
            Emails.Items.Clear();
            Notes.Text = "";
            MessageBox.Show("Registered Successfully");            
        }
        private void Additionals_Expanded(object sender, RoutedEventArgs e)
        {
            if (Personal.IsExpanded == true && Address.IsExpanded == true && Contact.IsExpanded == true)
            {
                Contact.IsExpanded = false;
            }
        }

        private void Contact_Expanded(object sender, RoutedEventArgs e)
        {
            if (Additionals.IsExpanded == true && Address.IsExpanded == true && Personal.IsExpanded == true)
            {
                Additionals.IsExpanded = false;
            }
        }

        private void Address_Expanded(object sender, RoutedEventArgs e)
        {
            if (Additionals.IsExpanded == true && Personal.IsExpanded == true && Contact.IsExpanded == true)
            {
                Additionals.IsExpanded = false;
            }

        }

        private void Personal_Expanded(object sender, RoutedEventArgs e)
        {
            if (Additionals.IsExpanded == true && Address.IsExpanded == true && Contact.IsExpanded == true)
            {
                Additionals.IsExpanded = false;
            }
        }
    }

}
