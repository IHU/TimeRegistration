using System;
using MediatR;

namespace Timelogger.Features.TimeLog.CreateTimeEntry
{
    public class CreateTimeLogEntryCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Hours { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
