using GreenvurcelDAL;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer customer = new Customer
            {
                PostalCode = "13412",
                CompanyName = "Compnay",
                FirstName = "Guy1231",
                LastName = "naor",
                BirthDate = "12/12/2007",
                HomeCountry = "hod123",
                HomeCity = "h123od",
                HomeStreet = "ho123d",
                WorkCountry = "h123od",
                WorkCity = "123hod",
                WorkStreet = "h123od",
                Grade = "1210",
                Phones = new List<Phone> { new Phone { PhoneType = "Mobile", PhoneNumber = "0" }, new Phone { PhoneType = "Home", PhoneNumber = "1" }, new Phone { PhoneType = "Mobile", PhoneNumber = "2" } },
                Emails = new List<Email> { new Email { EmailType = "Work", EmailAddress = "0" }, new Email { EmailType = "Personal", EmailAddress = "1" } }
            };
            Customer details = CustomerContext.Instance.LoadCustomerById("2", out bool succeeded);
            Console.WriteLine();
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
