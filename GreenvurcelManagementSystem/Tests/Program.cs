using GreenvurcelDAL;
using MongoDB.Bson;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace Tests
{
    class Program
    {
        public static void Upload(List<Customer> customers, List<CustomerProduct> customerProducts)
        {
            Customer highestCustomer = null;
            int i = 0;
            foreach (Customer customer in customers)
            {
                CustomerContext.Instance.InsertCustomerWithId(customer);
                if (i == 0)
                {
                    highestCustomer = new Customer { _id = customer._id };
                    i++;
                }
                else
                {
                    if (highestCustomer._id < customer._id)
                    {
                        highestCustomer._id = customer._id;
                    }
                }

            }
            CustomerContext.Instance.StarterCounterValue(highestCustomer._id);
            foreach (CustomerProduct customerProduct in customerProducts)
            {
                CustomerProductsContext.Instance.InsertCustomerProductWithId(customerProduct);
            }
        }

        public static void ReadCustomer()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            List<Customer> customers = new List<Customer>();
            List<CustomerProduct> customerProducts = new List<CustomerProduct>();

            using (var package = new ExcelPackage(new FileInfo(@"C:\Users\Guy\Downloads\לקוחות.xlsx")))
            {
                var firstSheet = package.Workbook.Worksheets["Sheet1"];

                int i = 2;
                while (i <= 5589)
                {
                    if (firstSheet.Cells["A" + i].Text != "")
                    {
                        List<Phone> phones = new List<Phone>();
                        if (firstSheet.Cells["K" + i].Text != "")
                        {
                            phones.Add(new Phone
                            {
                                PhoneType = "Mobile",
                                PhoneNumber = firstSheet.Cells["K" + i].Text
                            });
                        }
                        if (firstSheet.Cells["J" + i].Text != "")
                        {
                            phones.Add(new Phone
                            {
                                PhoneType = "Home",
                                PhoneNumber = firstSheet.Cells["J" + i].Text
                            });
                        }
                        if (firstSheet.Cells["I" + i].Text != "")
                        {
                            phones.Add(new Phone
                            {
                                PhoneType = "Work",
                                PhoneNumber = firstSheet.Cells["I" + i].Text
                            });
                        };

                        Customer customer = new Customer
                        {
                            _id = long.Parse(firstSheet.Cells["A" + i].Text),
                            FirstName = firstSheet.Cells["C" + i].Text,
                            LastName = firstSheet.Cells["B" + i].Text,
                            Grade = firstSheet.Cells["M" + i].Text,
                            HomeCountry = firstSheet.Cells["G" + i].Text,
                            HomeCity = firstSheet.Cells["E" + i].Text,
                            HomeStreet = firstSheet.Cells["D" + i].Text,
                            HomePostalCode = firstSheet.Cells["H" + i].Text,
                            Notes = firstSheet.Cells["N" + i].Text,
                            Phones = phones,
                            Emails = new List<Email>()
                        };
                        customers.Add(customer);
                    }
                    i++;
                    Console.WriteLine(i);
                }
            }
            using (var package = new ExcelPackage(new FileInfo(@"C:\Users\Guy\Downloads\דואל.xlsx")))
            {
                var firstSheet = package.Workbook.Worksheets["Sheet1"];
                int i = 2;
                while (i <= 6041)
                {
                    if (firstSheet.Cells["A" + i].Text != "")
                    {
                        Customer customer = customers.Find(customer => customer._id == long.Parse(firstSheet.Cells["A" + i].Text));
                        if (firstSheet.Cells["B" + i].Text != "")
                        {
                            Email email = new Email { EmailType = "Personal", EmailAddress = firstSheet.Cells["B" + i].Text };
                            customer.Emails.Add(email);
                        }
                    }
                    i++;
                    Console.WriteLine(i);
                }

                List<Customer> a = customers.FindAll(c => c.Emails.Count > 0);
                Console.WriteLine();
            }
            using (var package = new ExcelPackage(new FileInfo(@"C:\Users\Guy\Downloads\מקום עבודה.xlsx")))
            {
                var firstSheet = package.Workbook.Worksheets["Sheet1"];
                int i = 2;
                while (i <= 413)
                {
                    if (firstSheet.Cells["A" + i].Text != "")
                    {
                        Customer customer = customers.Find(customer => customer._id == long.Parse(firstSheet.Cells["A" + i].Text));
                        customer.WorkState = firstSheet.Cells["F" + i].Text;
                        customer.WorkCountry = firstSheet.Cells["H" + i].Text;
                        customer.WorkCity = firstSheet.Cells["E" + i].Text;
                        customer.WorkStreet = firstSheet.Cells["D" + i].Text;
                        customer.CompanyName = firstSheet.Cells["C" + i].Text;
                        customer.WorkPostalCode = firstSheet.Cells["G" + i].Text;
                        customer.Job = firstSheet.Cells["B" + i].Text;
                    }
                    i++;
                    Console.WriteLine(i);
                }
            }
            using (var package = new ExcelPackage(new FileInfo(@"C:\Users\Guy\Downloads\פריטים.xlsx")))
            {
                var firstSheet = package.Workbook.Worksheets["Sheet1"];
                int i = 2;
                while (i <= 8451)
                {
                    if (firstSheet.Cells["A" + i].Text != "")
                    {
                        CustomerProduct customerProduct = new CustomerProduct
                        {
                            CustomerID = long.Parse(firstSheet.Cells["A" + i].Text),
                            ProductName = firstSheet.Cells["B" + i].Text
                        };
                        customerProducts.Add(customerProduct);
                    }
                    i++;
                    Console.WriteLine(i);
                }
            }

            Upload(customers, customerProducts);
        }
        private static void OpenOutlook()
        {
            try
            {
                Outlook.Application oApp = new Outlook.Application();
                Outlook._MailItem mailItem = (Outlook._MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);
                mailItem.Subject = "This is the subject";
                mailItem.To = "someone@example.com";
                mailItem.Body = "This is the message.";
                mailItem.Display(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void Main(string[] args)
        {
            //ReadCustomer();
            OpenOutlook();
        }
    }
}


