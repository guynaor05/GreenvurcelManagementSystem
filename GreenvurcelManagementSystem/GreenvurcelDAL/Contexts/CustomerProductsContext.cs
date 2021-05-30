using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenvurcelDAL
{
    public class CustomerProductsContext
    {
        #region Constans

        private const string DATABASE_NAME = "GreenvurcelDB";
        private const string COLLECTION_NAME_CUSTOMER_PRODUCTS = "CustomerProducts";

        #endregion
        #region Events

        public event Action ProdcutAdded;

        #endregion
        #region Data Members

        private IMongoDatabase _database;

        #endregion

        #region Singleton Implementation

        private static CustomerProductsContext _instance;

        public static CustomerProductsContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CustomerProductsContext();
                }

                return _instance;
            }
        }

        private CustomerProductsContext()
        {
            var client = new MongoClient();
            _database = client.GetDatabase(DATABASE_NAME);
        }

        #endregion

        #region Public Methods

        public bool InsertCustomerProduct(CustomerProduct CustomerProduct)
        {
            try
            {
                var collection = _database.GetCollection<CustomerProduct>(COLLECTION_NAME_CUSTOMER_PRODUCTS);
                collection.InsertOne(CustomerProduct);
                ProdcutAdded?.Invoke();
                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }
        public void InsertCustomerProductWithId(CustomerProduct CustomerProduct)
        {
            var collection = _database.GetCollection<CustomerProduct>(COLLECTION_NAME_CUSTOMER_PRODUCTS);
            collection.InsertOne(CustomerProduct);
            ProdcutAdded?.Invoke();
        }
        public List<CustomerProduct> LoadCustomersProducts()
        {
            try
            {
                var collection = _database.GetCollection<CustomerProduct>(COLLECTION_NAME_CUSTOMER_PRODUCTS);
                return collection.Find(new BsonDocument()).ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }
        public List<CustomerProduct> LoadCustomerProducts(string id)
        {
            try
            {
                var collection = _database.GetCollection<CustomerProduct>(COLLECTION_NAME_CUSTOMER_PRODUCTS);
                List<CustomerProduct> customerProducts = collection.Find(new BsonDocument()).ToList();
                List<CustomerProduct> filteredCustomers = customerProducts.FindAll(product => product.CustomerID == long.Parse(id));
                return filteredCustomers;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public bool DeleteCustomerProduct(ObjectId id)
        {
            try
            {
                var collection = _database.GetCollection<CustomerProduct>(COLLECTION_NAME_CUSTOMER_PRODUCTS);
                var filter = Builders<CustomerProduct>.Filter.Eq("_id", id);
                collection.DeleteOne(filter);
                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }

        #endregion
    }
}
