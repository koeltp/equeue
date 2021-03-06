﻿using System;
using ECommon.Autofac;
using ECommon.Configurations;
using ECommon.JsonNet;
using ECommon.Log4Net;
using EQueue.Broker;
using EQueue.Configurations;

namespace QuickStart.BrokerServer
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeEQueue();
            BrokerController.Create().Start();
            Console.ReadLine();
        }

        static void InitializeEQueue()
        {
            var connectionString = @"Data Source=(localdb)\Projects;Integrated Security=true;Initial Catalog=EQueue;Connect Timeout=30;Min Pool Size=10;Max Pool Size=100";
            var queueStoreSetting = new SqlServerQueueStoreSetting
            {
                ConnectionString = connectionString
            };
            var messageStoreSetting = new SqlServerMessageStoreSetting
            {
                ConnectionString = connectionString,
                MessageStoreMaxHours = 1,
                IgnoreUnConsumedMessage = true,
                DeleteMessageInterval = 5000
            };
            var offsetManagerSetting = new SqlServerOffsetManagerSetting
            {
                ConnectionString = connectionString
            };

            Configuration
                .Create()
                .UseAutofac()
                .RegisterCommonComponents()
                .UseLog4Net()
                .UseJsonNet()
                .RegisterEQueueComponents()
                .UseSqlServerQueueStore(queueStoreSetting)
                .UseSqlServerMessageStore(messageStoreSetting)
                .UseSqlServerOffsetManager(offsetManagerSetting);
        }
    }
}
