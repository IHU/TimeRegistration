using System;
using System.ComponentModel.DataAnnotations;

namespace Timelogger.Entities
{
    public class TimeLogEntry
    {
        public int Id { get; set; }
        [MaxLength(100)]
		public string Name { get; set; }
		public int ProjectId { get; set; }
        public int UserId { get; set; }
        [MaxLength(200)]
		public string Description { get; set; }
        public double Hours { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
