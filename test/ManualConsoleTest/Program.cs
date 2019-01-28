using System;
using DBWatcher.Core.Entities;
using DBWatcher.Core.ScriptResult;
using DBWatcher.Core.Services;

namespace ManualConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TestServices();
            Console.ReadLine();
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