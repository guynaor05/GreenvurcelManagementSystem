using GreenvurcelDAL;
using MongoDB.Bson;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tests
{
    class Program
    {
        public static void read()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            List<Customer> customers = new List<Customer>();

            using (var package = new ExcelPackage(new FileInfo(@"C:\Users\Guy\Downloads\לקוחות.xlsx")))
            {
                var firstSheet = package.Workbook.Worksheets["Sheet1"];

                Console.WriteLine("Sheet 1 Data");
                Console.WriteLine($"Cell A2 Value   : {firstSheet.Cells["A2"].Text}");
                Console.WriteLine($"Cell B2 Value   : {firstSheet.Cells["B2"].Text}");
                Console.WriteLine("");
                int i = 2;
                while (firstSheet.Cells["A" + i].Text != "")
                {
                    List<Phone> phones = new List<Phone> { 
                        new Phone
                        {
                            PhoneType = "Mobile",
                            PhoneNumber = firstSheet.Cells["K" + i].Text
                        },
                        new Phone
                        {
                            PhoneType = "Home",
                            PhoneNumber = firstSheet.Cells["J" + i].Text
                        },
                        new Phone
                        {
                            PhoneType = "Work",
                            PhoneNumber = firstSheet.Cells["I" + i].Text
                        }
                    };

                    Customer customer = new Customer
                    {
                        _id = long.Parse(firstSheet.Cells["A" + i].Text),
                        FirstName = firstSheet.Cells["C" + i].Text ,
                        LastName =  firstSheet.Cells["B" + i].Text ,
                        Grade =  firstSheet.Cells["M" + i].Text ,
                        HomeCountry =  firstSheet.Cells["G" + i].Text ,
                        HomeCity =  firstSheet.Cells["E" + i].Text ,
                        HomeStreet =  firstSheet.Cells["D" + i].Text ,
                        PostalCode =  firstSheet.Cells["H" + i].Text ,
                        Notes =  firstSheet.Cells["N" + i].Text,
                        Phones = phones
                        //BirthDate = { firstSheet.Cells["C" + i].Text },
                        //WorkCountry = { firstSheet.Cells["C" + i].Text },
                        //WorkCity = { firstSheet.Cells["C" + i].Text },
                        //WorkStreet = { firstSheet.Cells["C" + i].Text },
                        //CompanyName = { firstSheet.Cells["C" + i].Text },
                    };
                    customers.Add(customer);
                    i++;
                    Console.WriteLine(i);
                }
            }
            using (var package = new ExcelPackage(new FileInfo(@"C:\Users\Guy\Downloads\דואל.xlsx")))
            {
                var firstSheet = package.Workbook.Worksheets["Sheet1"];
                int i = 2;
                while (firstSheet.Cells["A" + i].Text != "")
                {
                    List<Email> emails = new List<Email>();
                    if (firstSheet.Cells["B" + i].Text != "")
                    {
                        Email email = new Email { EmailType = "Personal", EmailAddress = firstSheet.Cells["B" + i].Text };
                        emails.Add(email);
                    }
                    Customer customer = customers.Find(customer => customer._id == long.Parse(firstSheet.Cells["A" + i].Text));
                    customer.Emails = emails;
                    //foreach (Customer customer in customers)
                    //{
                    //    if(customer._id == long.Parse(firstSheet.Cells["A" + i].Text))
                    //    {
                    //        customer.Emails = emails;
                    //        Console.WriteLine(i);
                    //    }
                    //}
                    i++;
                }
            }
        }
        static void Main(string[] args)
        {
            read();

            //Customer customer = new Customer
            //{
            //    PostalCode = "13412",
            //    CompanyName = "Compnay",
            //    FirstName = "Guy1231",
            //    LastName = "naor",
            //    BirthDate = "12/12/2007",
            //    HomeCountry = "hod123",
            //    HomeCity = "h123od",
            //    HomeStreet = "ho123d",
            //    WorkCountry = "h123od",
            //    WorkCity = "123hod",
            //    WorkStreet = "h123od",
            //    Grade = "1210",
            //    Phones = new List<Phone> { new Phone { PhoneType = "Mobile", PhoneNumber = "0" }, new Phone { PhoneType = "Home", PhoneNumber = "1" }, new Phone { PhoneType = "Mobile", PhoneNumber = "2" } },
            //    Emails = new List<Email> { new Email { EmailType = "Work", EmailAddress = "0" }, new Email { EmailType = "Personal", EmailAddress = "1" } }
            //};


            //Customer details = CustomerContext.Instance.LoadCustomerById("2", out bool succeeded);
            //Console.WriteLine();
            //CustomerContext.Instance.InsertCustomer(customer);

            //var a = CustomerContext.Instance.LoadCustomers();

            //var a1 = CustomerContext.Instance.LoadCustomerById("2");

            //CustomerContext.Instance.UpdateCustomer("1", customer);

            //var p = new CustomerProduct
            //{
            //    CustomerID = 2,
            //    ProductName = "a",
            //    CategoryName = "ABBBB"
            //};

            //CustomerProductsContext.Instance.DeleteCustomerProduct(new ObjectId("6001e2bcd661ffc5fbd0ccff"));

            Console.WriteLine();

        }
    }
}


