using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenvurcelDAL
{
    public class CustomerContext
    {
        #region Constants
        
        private const string DATABASE_NAME = "GreenvurcelDB";
        private const string COLLECTION_NAME_CUSTOMERS = "Customers";
        private const string COLLECTION_NAME_COUNTER = "Counter";

        #endregion

        private IMongoDatabase _database;
        private static CustomerContext _instance;

        #region Singleton Implementation

        public static CustomerContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CustomerContext();
                }

                return _instance;
            }
        }

        private CustomerContext()
        {
            var client = new MongoClient();
            _database = client.GetDatabase(DATABASE_NAME);
        }

        #endregion

        #region Public Methods

        public void InsertCustomer(Customer customer)
        {
            var collection = _database.GetCollection<Customer>(COLLECTION_NAME_CUSTOMERS);
            long id = GetNextCounterValue();
            customer._id = id;

            collection.InsertOne(customer);
        }

        private long GetNextCounterValue()
        {
            var collection = _database.GetCollection<Counter>(COLLECTION_NAME_COUNTER);
            var filter = Builders<Counter>.Filter.Eq(col => col.Id, "Counter");

            // did not find counter document, we need to create new one
            if (collection.Find(filter).FirstOrDefault() == null)
            {
                collection.InsertOne(new Counter { Id = "Counter", Value = 1 });
                return 1;
            }

            var update = Builders<Counter>.Update.Inc(col => col.Value, 1);
            collection.FindOneAndUpdate(filter, update);
            
            return collection.Find(filter).FirstOrDefault().Value;
        }

        public List<Customer> LoadCustomers()
        {
            var collection = _database.GetCollection<Customer>(COLLECTION_NAME_CUSTOMERS);
            return collection.Find(new BsonDocument()).ToList();
        }

        /// <summary>
        /// returns a customer with id if exist and null otherweise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer LoadCustomerById(string id)
        {
            var collection = _database.GetCollection<Customer>(COLLECTION_NAME_CUSTOMERS);
            var filter = Builders<Customer>.Filter.Eq("_id", long.Parse(id));
            return collection.Find(filter).FirstOrDefault();
        }

        public void UpdateCustomer(string id, Customer newCustomer)
        {
            var collection = _database.GetCollection<Customer>(COLLECTION_NAME_CUSTOMERS);
            var filter = Builders<Customer>.Filter.Eq(customer => customer._id, long.Parse(id));
            newCustomer._id = long.Parse(id);

            collection.ReplaceOne(filter, newCustomer);
        }
        #endregion
    }
}
