using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Timelogger.Entities
{
	public class Project
	{
		public Project()
		{
		}

		public Project(int id, string name, int userId, List<TimeLogEntry> timeEntries)
		{
			Id = id;
			Name = name;
			UserId = userId;
			TimeEntries = timeEntries;
		}

		public Project(int id, string name, List<TimeLogEntry> timeEntries) : this()
		{
			Id = id;
			Name = name;
			TimeEntries = timeEntries;
		}

		public int Id { get; set; }
		
		[MaxLength(100)]
		public string Name { get; set; }
		public DateTime EndDate { get; set; }
		public int CustomerId { get; set; }
		public int UserId { get; set; }

		[MaxLength(300)]
		public string Notes { get; set; }
		public bool IsCompleted { get; set; }
		public List<TimeLogEntry> TimeEntries {get;set;}
	}
}
