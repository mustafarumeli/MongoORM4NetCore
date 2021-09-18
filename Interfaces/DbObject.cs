using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoORM4NetCore.Interfaces
{
    public abstract class DbObject : IDbObject
    {
        [BsonElement("_id")]
        public string Id { get; set; }
        public DateTime CreationDate { get; set; }

        protected DbObject()
        {
            Id = Guid.NewGuid().ToString();
            CreationDate = DateTime.Now;
        }

        public override string ToString()
        {
            var properties = GetType().GetProperties();
            var fields = GetType().GetFields();
            var returnValue = new StringBuilder();
            WriteFields(properties);
            WriteFields(fields);
            returnValue.Remove(returnValue.Length - 2, 2);
            void WriteFields(IEnumerable<dynamic> enumerable)
            {
                foreach (var property in enumerable)
                {
                    var value = property.GetValue(this);
                    if (value != null)
                    {
                        returnValue.Append(property.Name + ":" + value + ", ");
                    }
                }
            }
            return returnValue.ToString();
        }
    }

    public interface IDbObject
    {
        string Id { get; set; }
        DateTime CreationDate { get; set; }
    }
}
