﻿using System;
using System.Collections.Generic;
using System.Linq;
using MongoORM4NetCore.Structs;

namespace MongoORM4NetCore
{
    internal class MongoConnectionObjectBuilder
    {
        private readonly MongoConnectionObject _mongoConnectionObject;
        private static MongoConnectionObject MongoConnectionObject;

        internal MongoConnectionObjectBuilder()
        {
            _mongoConnectionObject = new MongoConnectionObject();
            MongoConnectionObject = _mongoConnectionObject;
        }

        internal MongoConnectionObjectBuilder GiveUserName(string userName)
        {
            _mongoConnectionObject.UserName = userName;
            return this;
        }
        internal MongoConnectionObjectBuilder GivePassword(string password)
        {
            _mongoConnectionObject.Password = password;
            return this;
        }

        internal MongoConnectionObjectBuilder GiveHost(string host)
        {
            var mainIpConfig = _mongoConnectionObject.MainIpConfig;
            mainIpConfig.Host = host;
            return this;
        }
        internal MongoConnectionObjectBuilder GivePort(int port)
        {
            var mainIpConfig = _mongoConnectionObject.MainIpConfig;
            mainIpConfig.Port = port;
            return this;
        }
        internal MongoConnectionObjectBuilder GiveDatabaseName(string databaseName)
        {
            _mongoConnectionObject.DatabaseName = databaseName;
            return this;
        }
        internal MongoConnectionObjectBuilder AddConnectionOptions(params Tuple<string, string>[] options)
        {
            foreach (var option in options)
            {
                _mongoConnectionObject.ConnectionOption.Add(option.Item1, option.Item2);
            }
            return this;
        }
        internal MongoConnectionObjectBuilder AddReplica(string host, int port)
        {
            _mongoConnectionObject.ReplicasIpConfig.Add(new MongoConnectionObject.IpConfig
            {
                Host = host,
                Port = port
            });
            return this;
        }
        internal MongoConnectionObjectBuilder AddReplica(IEnumerable<MongoConnectionStringReplicas> connectionStringReplicas)
        {
            _mongoConnectionObject.ReplicasIpConfig.AddRange(connectionStringReplicas.Select(x => (MongoConnectionObject.IpConfig)x));
            return this;
        }
        public static explicit operator MongoConnectionObject(MongoConnectionObjectBuilder builder)
        {
            return MongoConnectionObject;
        }
    }
}