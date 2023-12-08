using System;
using System.Collections.Generic;

namespace Timelogger.Entities
{
	public class Project
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime EndDate { get; set; }
		public int CustomerId { get; set; }
		public int UserId { get; set; }
		public string Notes { get; set; }
		public bool IsFinished { get; set; }
		public List<TimeLogEntry> TimeEntries {get;set;}
	}
}
