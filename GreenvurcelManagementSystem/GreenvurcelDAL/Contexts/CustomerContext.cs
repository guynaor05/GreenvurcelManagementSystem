using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenvurcelDAL
{
    public class CustomerContext
    {
        private IMongoDatabase db;
        public CustomerContext(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }
        public void InsertRecord<T>(string table, T record)
        {
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(record);
        }
        public List<T> LoadRecords<T>(string table)
        {
            var collection = db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();
        }
        public T LoadRecordById<T>(string table, string id)
        {
            var collection = db.GetCollection<T>(table);
            var Filter = Builders<T>.Filter.Eq("Id", id);
            return collection.Find(Filter).First();
        }

        [Obsolete]
        public void UpsertRecord<T>(string table, string id, T record)
        {
            var collection = db.GetCollection<T>(table);
            var result = collection.ReplaceOne(new BsonDocument("_id", id), record, new UpdateOptions { IsUpsert = true });
        }
        public void DeleteRecord<T>(string table, string id)
        {
            var collection = db.GetCollection<T>(table);
            var Filter = Builders<T>.Filter.Eq("Id", id);
            collection.DeleteOne(Filter);
        }
    }   
}
