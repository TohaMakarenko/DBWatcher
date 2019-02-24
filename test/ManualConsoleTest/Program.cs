﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using DBWatcher.Core.Entities;
using DBWatcher.Core.Repositories;
using DBWatcher.Core.Results;
using DBWatcher.Core.Services;
using DBWatcher.Infrastructure.Data;
using DBWatcher.Infrastructure.Data.Repositories;

namespace ManualConsoleTest
{
    
    class Program
    {
        const string ConnectionString = "mongodb://localhost:27017/DbWatcherTest";
        
        static void Main(string[] args)
        {
            TestRepos();
            Console.ReadLine();
        }

        static async void TestRepos()
        {
            var sqlConn = new SqlConnection();
            var trans = sqlConn.BeginTransaction(IsolationLevel.Chaos);
        }

        static async Task TestServices()
        {
            var props = new ConnectionProperties() {
                Server = @"localhost\SQLEXPRESS",
                Login = "sa",
                Password = "sa",
                IsPasswordEncrypted = false
            };

            var conPropsService = new ConnectionPropertiesService(null, new CryptoManager());
            var scriptService = new ScriptService(conPropsService);
            var executor = scriptService.GetScriptExecutor(props);
            var script = new Script() {
                Body = "select * from sys.databases"
            };
            var result = await executor.ExecuteScriptMultiple(script.Body);
            PrintScriptMultipleResult(result);
        }

        static void PrintScriptMultipleResult(ScriptMultipleResult result)
        {
            if (!result.IsSuccess) {
                Console.WriteLine("ERRORS: ");
                foreach (var error in result.Errors) {
                    Console.WriteLine(error);
                }
            }
            else {
                foreach (var dataSet in result.Data) {
                    Console.WriteLine();
                    foreach (var row in dataSet) {
                        Console.WriteLine(row);
                    }
                }
            }
        }
    }
}