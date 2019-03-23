using DBWatcher.Core.Enums;
using DBWatcher.Core.ScriptExecutor;
using Microsoft.Extensions.DependencyInjection;

namespace DBWatcher.Core.Execution
{
    public static class IServiceCollectionExtension
    {
        public static void AddExecution(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionManager>(provider => {
                var manager = new ConnectionManager();
                manager.AddBuilder(RDBMS.MicrosoftSqlServer, new ConnectionBuilderSqlServer());
                return manager;
            });

            services.AddSingleton<IScriptExecutorManager, ScriptExecutorManager>();
        }
    }
}