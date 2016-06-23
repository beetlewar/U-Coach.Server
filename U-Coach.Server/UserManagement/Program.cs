﻿using PVDevelop.UCoach.Server.Logging;
using StructureMap;
using System;

namespace PVDevelop.UCoach.Server.UserManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new Logger<Program>();
            logger.Debug("Application started");

            if (args.Length > 0)
            {
                try
                {
                    var executor = ExecutorFactory.CreateAndSetupExecutor(args);
                    executor.Execute();
                    Console.WriteLine(executor.GetSuccessString());
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Ошибка при обработке Executor.");
                    Console.WriteLine("Failed");
                    new HelpExecutor().PrintHelp();
                }
            }
            else
            {
                try
                {
                    new HelpExecutor().PrintHelp();
                }
                catch(Exception  ex)
                {
                    logger.Error(ex, "Ошибка при выводе help.");
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
