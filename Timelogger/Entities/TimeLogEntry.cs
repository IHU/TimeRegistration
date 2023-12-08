using System;
namespace Timelogger.Entities
{
    public class TimeLogEntry
    {
        public int Id { get; set; }
        public string Name { get; set; }
		public int ProjectId { get; set; }
        public int UserId { get; set; }
		public string Description { get; set; }
        public double Hours { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
