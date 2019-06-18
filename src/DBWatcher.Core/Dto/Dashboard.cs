using System.Collections;
using System.Collections.Generic;

namespace DBWatcher.Core.Dto
{
    public class Dashboard : BaseDto<int>
    {
        public string Name { get; set; }
        public IEnumerable<ChartSettings> Charts { get; set; }
    }

    public class ChartSettings
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int JobId { get; set; }
        public int LogLimit { get; set; }
        public int UpdateInterval { get; set; }
        public IEnumerable<SeriesSettings> Series { get; set; }
    }

    public class SeriesSettings
    {
        public string Label { get; set; }
        public string Column { get; set; }
        public string Color { get; set; }
    }
}