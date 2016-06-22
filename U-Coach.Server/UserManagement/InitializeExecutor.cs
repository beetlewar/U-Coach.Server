﻿using PVDevelop.UCoach.Server.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVDevelop.UCoach.Server.UserManagement
{
    public class InitializeExecutor : IExecutor
    {
        public string[] ArgumentsNames
        {
            get
            {
                return null;
            }
        }

        public string Command
        {
            get
            {
                return "initialize";
            }
        }

        public string Description
        {
            get
            {
                return "Инициализировать систему";
            }
        }

        public void Execute()
        {
        }

        public string GetSuccessString()
        {
            return "Система инициализирована";
        }

        public void Setup(string[] arguments)
        {
            foreach(var mongoInit in ExecutorContainer.Instance.Container.GetAllInstances<IMongoInitializer>())
            {
                mongoInit.Initialize();
            }
        }
    }
}
