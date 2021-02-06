using System.Collections.Generic;
using MongoDB.Bson;

namespace MongoORM4NetCore.Interfaces
{
    public interface IRepository<T> where T : DbObject
    {
        bool Insert(T entity);
        bool InsertMany(params T[] entities);
        bool Delete(string id);
        bool Update(string id, T entity);
        bool Upsert(T entity);
        List<T> GetAll(BsonDocument filter);
        T GetOne(string id);
        T GetOne<T1>(string fieldName, T1 value);
        List<T> Search(string value, string field);
        List<T> MultipleFieldSearch(string value, params string[] fields);
    }
}