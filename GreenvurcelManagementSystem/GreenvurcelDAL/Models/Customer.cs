using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace GreenvurcelDAL
{
    public class Customer
    {
        [BsonId]
        public long _id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Grade { get; set; }
        public string HomeCountry { get; set; }
        public string HomeCity { get; set; }
        public string HomeStreet { get; set; }
        public string HomePostalCode { get; set; }
        public string WorkPostalCode { get; set; }
        public string WorkCountry { get; set; }
        public string WorkCity { get; set; }
        public string WorkStreet { get; set; }
        public string CompanyName { get; set; }
        public string HomeState { get; set; }
        public string WorkState { get; set; }
        public string Job { get; set; }
        public List<Phone> Phones { get; set; }
        public List<Email> Emails { get; set; }
        public string Notes { get; set; }

    }
}

