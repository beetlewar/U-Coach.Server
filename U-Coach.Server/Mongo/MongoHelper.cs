﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace PVDevelop.UCoach.Server.Mongo
{
    public static class MongoHelper
    {
        /// <summary>
        /// Возвращает имя для коллекции типа T
        /// </summary>
        public static string GetCollectionName<T>()
        {
            var mongoCollectionAttr = 
                (MongoCollectionAttribute)typeof(T).
                GetCustomAttributes(typeof(MongoCollectionAttribute), true).
                SingleOrDefault();

            if(mongoCollectionAttr != null)
            {
                return mongoCollectionAttr.Name;
            }

            return typeof(T).Name;
        }

        /// <summary>
        /// Возвращает версию типа данных T
        /// </summary>
        public static int GetDataVersion<T>()
        {
            var mongoDataVersionAttr =
                (MongoDataVersionAttribute)typeof(T).
                GetCustomAttributes(typeof(MongoDataVersionAttribute), true).
                SingleOrDefault();

            if(mongoDataVersionAttr == null)
            {
                throw new InvalidOperationException();
            }
            return mongoDataVersionAttr.Version;
        }

        public static IMongoCollection<T> GetCollection<T>(IMongoConnectionSettings settings)
        {
            var builder = new MongoUrlBuilder(settings.ConnectionString);

            var mongoClient = new MongoClient(builder.ToMongoUrl());
            var db = mongoClient.GetDatabase(builder.DatabaseName);

            var collectionName = GetCollectionName<T>();

            return db.GetCollection<T>(collectionName);
        }
    }
}