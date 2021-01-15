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

        public void InsertCustomerProduct(CustomerProduct CustomerProduct)
        {
            var collection = _database.GetCollection<CustomerProduct>(COLLECTION_NAME_CUSTOMER_PRODUCTS);
            collection.InsertOne(CustomerProduct);
        }

        public List<CustomerProduct> LoadCustomerProducts()
        {
            var collection = _database.GetCollection<CustomerProduct>(COLLECTION_NAME_CUSTOMER_PRODUCTS);
            return collection.Find(new BsonDocument()).ToList();
        }

        public void DeleteCustomerProduct(ObjectId id)
        {
            var collection = _database.GetCollection<CustomerProduct>(COLLECTION_NAME_CUSTOMER_PRODUCTS);
            var filter = Builders<CustomerProduct>.Filter.Eq("_id", id);
            collection.DeleteOne(filter);
        }

        #endregion
    }
}
