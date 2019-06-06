using System;
using System.Threading.Tasks;
using DBWatcher.Core;
using DBWatcher.Core.Dto;
using DBWatcher.Core.Services;
using Quartz;

namespace DBWatcher.Scheduling.Quartz
{
    public class ScriptJob : IJob, IDisposable
    {
        [NonSerialized] private readonly IUnitOfWork _unitOfWork;
        private readonly IScriptService _scriptService;
        [NonSerialized] private ConnectionProperties _connection;
        [NonSerialized] private Script _script;

        public ScriptJob(IUnitOfWork unitOfWork, IScriptService scriptService)
        {
            _unitOfWork = unitOfWork;
            _scriptService = scriptService;
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
            var executor = _scriptService.GetScriptExecutor(connection);
            var startTime = DateTime.Now;
            var result = await executor.ExecuteScriptMultiple(script.Body, Job.Parameters);
            var finishTime = DateTime.Now;
            var logRecord = new JobLog {
                JobId = Job.Id,
                StartTime = startTime,
                FinishTime = finishTime,
                Result = result
            };
            await _unitOfWork.JobLogRepository.Insert(logRecord);
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