using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBWatcher.Core;
using DBWatcher.Core.Dto;
using DBWatcher.Core.Execution;
using DBWatcher.Core.Services;
using Quartz;

namespace DBWatcher.Scheduling.Quartz
{
    public class ScriptJob : IJob, IDisposable
    {
        [NonSerialized] private readonly IUnitOfWork _unitOfWork;
        private readonly IScriptService _scriptService;
        [NonSerialized] private List<ConnectionProperties> _connections;
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
            await Task.WhenAll(Job.ExecutionContexts.Select(x => ExecuteScript(Job, script, x)));
        }

        private async Task ExecuteScript(Job job, Script script, JobExecutionContext context)
        {
            var connection = await GetConnection(context.ConnectionId);
            var executor = _scriptService.GetScriptExecutor(connection);
            var startTime = DateTime.Now;
            var result = await executor.ExecuteScriptMultiple(script.Body, Job.Parameters);
            var finishTime = DateTime.Now;
            var logRecord = new JobLog {
                JobId = Job.Id,
                Context = context,
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

        private async Task<ConnectionProperties> GetConnection(int id)
        {
            var connections = _connections ?? (_connections =
                                  await _unitOfWork.ConnectionPropertiesRepository.Get(Job.ExecutionContexts.Select(x => x.ConnectionId).ToArray()));
            return connections.FirstOrDefault(x => x.Id == id);
        }

        private void OnScriptUpdated(Script script)
        {
            if (script.Id == _script.Id) _script = script;
        }

        private void OnConnectionUpdated(ConnectionProperties connection)
        {
            var conn = _connections?.FirstOrDefault(x => x.Id == connection.Id);
            if (conn != null) {
                _connections.Remove(conn);
                _connections.Add(connection);
            }
        }
    }
}