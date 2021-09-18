using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoORM4NetCore.Interfaces;
using MongoORM4NetCore.Structs;

namespace MongoORM4NetCore
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOhm(this IServiceCollection services, string connectionString, string databaseName,Type crudAssemblyType, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            MongoDbConnection.InitializeAndStartConnection(connectionString, databaseName);
            return InjectCrud(services, crudAssemblyType, serviceLifetime);
        }

        private static IServiceCollection InjectCrud(IServiceCollection services, Type crudAssemblyType,
            ServiceLifetime serviceLifetime)
        {
            var assembly = crudAssemblyType.Assembly;
            var dbObjectType = typeof(IDbObject);
            var entityTypes = assembly.GetExportedTypes()
                .Where(x => x.IsAssignableTo(dbObjectType) && !x.IsInterface && !x.IsAbstract);
            foreach (var entityType in entityTypes)
            {
                var interfaceType = typeof(IRepository<>).MakeGenericType(entityType);
                var objectType = typeof(Crud<>).MakeGenericType(entityType);
                services.Add(new ServiceDescriptor(interfaceType, objectType, serviceLifetime));
            }

            return services;
        }

        public static IServiceCollection AddOhm(this IServiceCollection services,Type crudAssemblyType,
            string databaseName = "",
            string serverIP = "localhost",
            int port = 27017,
            string userName = "",
            string password = "",
            IDictionary<string, string> connectionStringOptions = null,
            IEnumerable<MongoConnectionStringReplicas> connectionStringReplicas = null,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            MongoDbConnection.InitializeAndStartConnection(databaseName, serverIP, port, userName,password, connectionStringOptions,connectionStringReplicas );
            return InjectCrud(services, crudAssemblyType, serviceLifetime);
        }
    }
}