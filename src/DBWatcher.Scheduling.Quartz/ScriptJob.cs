using System;
using System.Threading.Tasks;
using DBWatcher.Core;
using DBWatcher.Core.Dto;
using Quartz;

namespace DBWatcher.Scheduling.Quartz
{
    public class ScriptJob : IJob, IDisposable
    {
        [NonSerialized] private readonly IUnitOfWork _unitOfWork;
        [NonSerialized] private ConnectionProperties _connection;
        [NonSerialized] private Script _script;

        public ScriptJob(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.ScriptRepository.OnUpdate += OnScriptUpdated;
            _unitOfWork.ConnectionPropertiesRepository.OnUpdate += OnConnectionUpdated;
        }

        public Job Job { get; set; }

        public void Dispose()
        {
            _unitOfWork.ScriptRepository.OnUpdate -= OnScriptUpdated;
            _unitOfWork.ConnectionPropertiesRepository.OnUpdate -= OnConnectionUpdated;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var script = await GetScript();
            var connection = await GetConnection();
            Console.WriteLine(script.Body);
        }

        private async Task<Script> GetScript()
        {
            return _script ?? (_script = await _unitOfWork.ScriptRepository.Get(Job.ScriptId));
        }

        private async Task<ConnectionProperties> GetConnection()
        {
            return _connection ?? (_connection = await _unitOfWork.ConnectionPropertiesRepository.Get(Job.ConnectionId));
        }

        private void OnScriptUpdated(Script script)
        {
            if (script.Id == _script.Id) _script = script;
        }

        private void OnConnectionUpdated(ConnectionProperties connection)
        {
            if (connection.Id == _connection.Id) _connection = connection;
        }
    }
}