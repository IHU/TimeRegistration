using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timelogger.Repositories;

namespace Timelogger.Features.TimeLog.DeleteTimeEntry
{
    public class DeleteTimeLogEntryCommandHandler : IRequestHandler<DeleteTimeLogEntryCommand, bool>
    {
        private readonly ITimeLogEntryRepository _repository;

        public DeleteTimeLogEntryCommandHandler(ITimeLogEntryRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteTimeLogEntryCommand request, CancellationToken cancellationToken)
        {
            var isDeleted = await _repository.DeleteTimeLogEntry(request.Id);
            return isDeleted;
        }
    }
}
