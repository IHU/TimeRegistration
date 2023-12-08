using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timelogger.Entities;
using Timelogger.Repositories;

namespace Timelogger.Features.TimeLog.CreateTimeEntry
{
    public class CreateTimeLogEntryCommandHandler : IRequestHandler<CreateTimeLogEntryCommand, int>
    {
        private readonly ITimeLogEntryRepository _repository;

        public CreateTimeLogEntryCommandHandler(ITimeLogEntryRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateTimeLogEntryCommand request, CancellationToken cancellationToken)
        {
            var timeLogEntry = new TimeLogEntry()
            {
                ProjectId = request.ProjectId,
                Description = request.Description,
                Name = request.Name,
                EntryDate = request.EntryDate,
                UserId = request.UserId,
                Hours = request.Hours,
            };
            var timeLog = await _repository.CreateTimeLogEntryAsync(timeLogEntry);
            return timeLog.Id;
        }
    }
}
