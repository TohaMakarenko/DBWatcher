using System;
using System.Threading.Tasks;
using DBWatcher.Core;
using DBWatcher.Core.Dto;
using Quartz;

namespace DBWatcher.Scheduling.Quartz
{
    public class ScriptJob : IJob, IDisposable
    {
        public ScriptJob(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            UnitOfWork.ScriptRepository.OnUpdate += OnScriptUpdated;
            UnitOfWork.ConnectionPropertiesRepository.OnUpdate += OnConnectionUpdated;
        }

        public IUnitOfWork UnitOfWork { get; }
        private Script Script { get; set; }
        private ConnectionProperties Connection { get; set; }

        public Job Job { get; set; }

        public void Dispose()
        {
            UnitOfWork.ScriptRepository.OnUpdate -= OnScriptUpdated;
            UnitOfWork.ConnectionPropertiesRepository.OnUpdate -= OnConnectionUpdated;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var script = await GetScript();
            var connection = await GetConnection();
            Console.WriteLine(script.Body);
        }

        private async Task<Script> GetScript()
        {
            return Script ?? (Script = await UnitOfWork.ScriptRepository.Get(Job.ScriptId));
        }

        private async Task<ConnectionProperties> GetConnection()
        {
            return Connection ?? (Connection = await UnitOfWork.ConnectionPropertiesRepository.Get(Job.ConnectionId));
        }

        private void OnScriptUpdated(Script script)
        {
            if (script.Id == Script.Id) Script = script;
        }

        private void OnConnectionUpdated(ConnectionProperties connection)
        {
            if (connection.Id == Connection.Id) Connection = connection;
        }
    }
}