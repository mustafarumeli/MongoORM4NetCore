using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoORM4NetCore.Interfaces
{
    public interface IRepository<T> : IInsert<T>, IDelete, IUpdate<T>, IRead<T>
        where T : IDbObject
    {
        string TableName { get; }
    }

    public interface IInsert<in T> where T : IDbObject
    {
        bool Insert(T entity);
        bool InsertMany(params T[] entities);
    }

    public interface IDelete
    {
        bool Delete(string id);
    }

    public interface IUpdate<in T> where T : IDbObject
    {
        bool Update(T entity);
        bool Upsert(T entity);
    }

    public interface IRead<T> where T : IDbObject
    {
        IEnumerable<T> GetAll(BsonDocument filter);
        T GetOne(string id);
        T GetOne<T1>(string fieldName, T1 value);
        IEnumerable<T> Search(string value, string field);
        IEnumerable<T> MultipleFieldSearch(string value, params string[] fields);
        IEnumerable<T> Search(Expression<Func<T, bool>> expression);
        IFindFluent<T, T> Find(Expression<Func<T, bool>> filter, FindOptions options = null);
    }
}