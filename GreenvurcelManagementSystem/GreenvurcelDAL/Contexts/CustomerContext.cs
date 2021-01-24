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
        private const string COLLECTION_NAME_CUSTOMER_PRODUCTS = "CustomerProducts";

        #endregion

        #region MyRegion

        private IMongoDatabase _database;
        private static CustomerContext _instance;

        #endregion

        #region Events
        
        public event Action CustomerAdded;
        public event Action CustomerRemoved;
        public event Action CustomerUpadted;

        #endregion

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
            CustomerAdded?.Invoke();
        }

        public List<Customer> LoadCustomers()
        {
            try
            {
                var collection = _database.GetCollection<Customer>(COLLECTION_NAME_CUSTOMERS);
                return collection.Find(new BsonDocument()).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// returns a customer with id if exist and null otherweise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer LoadCustomerById(string id, out bool succeeded)
        {
            succeeded = true;

            try
            {
                var collection = _database.GetCollection<Customer>(COLLECTION_NAME_CUSTOMERS);
                var filter = Builders<Customer>.Filter.Eq("_id", long.Parse(id));
                return collection.Find(filter).FirstOrDefault();
            }
            catch (Exception)
            {
                succeeded = false;
                return null;
            }
        }

        public bool UpdateCustomer(string id, Customer newCustomer)
        {
            try
            {
                var collection = _database.GetCollection<Customer>(COLLECTION_NAME_CUSTOMERS);
                var filter = Builders<Customer>.Filter.Eq(customer => customer._id, long.Parse(id));
                newCustomer._id = long.Parse(id);
                collection.ReplaceOne(filter, newCustomer);
                CustomerUpadted?.Invoke();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteCustomer(long id)
        {
            try
            {
                var collection = _database.GetCollection<Customer>(COLLECTION_NAME_CUSTOMERS);
                var filter = Builders<Customer>.Filter.Eq("_id", id);
                var collectionForProducts = _database.GetCollection<CustomerProduct>(COLLECTION_NAME_CUSTOMER_PRODUCTS);
                var filterForProducts = Builders<CustomerProduct>.Filter.Eq("CustomerID", id);
                collection.DeleteOne(filter);
                collectionForProducts.DeleteMany(filterForProducts);
                CustomerRemoved?.Invoke();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Private Methods

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

        #endregion
    }
}
