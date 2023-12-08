using System.Collections.Generic;
using Timelogger.Entities;

namespace Timelogger.Features.Projects
{
    public class ProjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EndDate { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string Notes { get; set; }
        public bool IsCompleted { get; set; }
        public double HoursSpend { get; set; }

        public List<TimeLogEntry> TimeEntries { get; set; }
    }
}
