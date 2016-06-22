﻿using MongoDB.Driver;
using NUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVDevelop.UCoach.Server.Mongo.Tests
{
    [TestFixture]
    [Integration]
    public class MetaInitializerIntegrTests
    {
        [Test]
        public void Initialize_NotInitialized_CreatesCollectionWithIndex()
        {
            var settings = TestMongoHelper.CreateSettings();

            TestMongoHelper.WithDb(settings, contextDb =>
            {
                var initializer = new MetaInitializer(settings);
                initializer.Initialize();

                // проверяем индекс
                var collection = MongoHelper.GetCollection<CollectionVersion>(settings);
                var indexName = MongoHelper.GetIndexName<CollectionVersion>(nameof(CollectionVersion.Name));
                var index = collection.Indexes.List().ToList().FirstOrDefault(i => i["name"] == indexName);

                Assert.NotNull(index);
                Assert.IsTrue(MongoHelper.IsUniqueIndex(index));
            });
        }
    }
}