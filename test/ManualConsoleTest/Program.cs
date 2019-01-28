using System;
using DBWatcher.Core.Entities;
using DBWatcher.Core.Repositories;
using DBWatcher.Core.ScriptResult;
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
            MongoConnectionManager.ConnectionString = ConnectionString;
            IConnectionPropertiesRepository cpRepo = new ConnectionPropertiesRepository();
            IScriptRepository scRepo = new ScriptRepository();
            var conProps = new ConnectionProperties() {
                Name = "TestConnectionProps1",
                Login = "sa",
                Password = "sa",
                Server = @"localhost\SQLEXPRESS"
            };
            await cpRepo.InsertConnection(conProps);
            var script = new Script() {
                Name = "ScriptTest1",
                Author = "Author1",
                Body = "select * from ErrorLog",
                Description = "Script description"
            };
            await scRepo.InsertScript(script);
        }

        static async void TestServices()
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
            var result = await executor.ExecuteScript(script);
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