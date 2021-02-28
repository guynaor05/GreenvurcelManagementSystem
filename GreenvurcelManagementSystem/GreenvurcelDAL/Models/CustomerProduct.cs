
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GreenvurcelDAL
{
    public class CustomerProduct
    {
        [BsonId]
        public ObjectId _id;
        public long CustomerID { get; set; }
        public string ProductName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CategoryName { get; set; }
        public bool IsObject { get; set; }
    }
}
