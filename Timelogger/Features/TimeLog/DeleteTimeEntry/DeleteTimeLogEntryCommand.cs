using MediatR;

namespace Timelogger.Features.TimeLog.DeleteTimeEntry
{
    public class DeleteTimeLogEntryCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
