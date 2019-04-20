using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DBWatcher.Core.Dto;
using DBWatcher.Core.Results;
using DBWatcher.Core.Services;

namespace ManualConsoleTest
{
    internal class Program
    {
        private const string ConnectionString = "mongodb://localhost:27017/DbWatcherTest";

        private static void Main(string[] args)
        {
            TestRepos();
            Console.ReadLine();
        }

        private static void TestRepos()
        {
            var sqlConn = new SqlConnection();
            var trans = sqlConn.BeginTransaction(IsolationLevel.Chaos);
        }

        private static async Task TestServices()
        {
            var props = new ConnectionProperties {
                Server = @"localhost\SQLEXPRESS",
                Login = "sa",
                Password = "sa",
                IsPasswordEncrypted = false
            };

            var conPropsService = new ConnectionPropertiesService(null, new CryptoManager());
            var scriptService = new ScriptService(conPropsService, null);
            var executor = scriptService.GetScriptExecutor(props);
            var script = new Script {
                Body = "select * from sys.databases"
            };
            var result = await executor.ExecuteScriptMultiple(script.Body);
            PrintScriptMultipleResult(result);
        }

        private static void PrintScriptMultipleResult(ScriptMultipleResult result)
        {
            if (!result.IsSuccess) {
                Console.WriteLine("ERRORS: ");
                foreach (var error in result.Errors) Console.WriteLine(error);
            }
            else {
                foreach (var dataSet in result.Data) {
                    Console.WriteLine();
                    foreach (var row in dataSet) Console.WriteLine(row);
                }
            }
        }
    }
}