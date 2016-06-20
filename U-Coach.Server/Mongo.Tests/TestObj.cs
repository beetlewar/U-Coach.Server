﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVDevelop.UCoach.Server.Mongo.Tests
{
    [MongoCollection("TestObj")]
    [MongoDataVersion(456)]
    internal sealed class TestObj : IHaveId
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

}