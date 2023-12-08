using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Timelogger.Entities
{
	public class Project
	{
		public int Id { get; set; }
		
		[MaxLength(100)]
		public string Name { get; set; }
		public DateTime EndDate { get; set; }
		public int CustomerId { get; set; }
		public int UserId { get; set; }

		[MaxLength(300)]
		public string Notes { get; set; }
		public bool IsFinished { get; set; }
		public List<TimeLogEntry> TimeEntries {get;set;}
	}
}
