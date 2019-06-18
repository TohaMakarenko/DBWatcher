namespace DBWatcher.Core.Dto.Jobs
{
    public class JobLogSearchFilter
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public int JobId { get; set; }
        public int ConnectionId { get; set; }
        public string Database { get; set; }
    }
}