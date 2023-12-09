using System;
using System.Globalization;
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
			string[] hoursParts = request.Hours.ToString(CultureInfo.InvariantCulture).Split('.');
			double hoursTotal;

			if (hoursParts.Length == 2)
			{
				int hours = int.Parse(hoursParts[0]);
				int minutes = int.Parse(hoursParts[1]);

				var totalMinute = hours * 60 + minutes;

				var span = System.TimeSpan.FromMinutes(totalMinute);


				hoursTotal = Convert.ToDouble(span.TotalHours);
			}
			else
			{
				hoursTotal = int.Parse(request.Hours);
			}



			var timeLogEntry = new TimeLogEntry()
			{
				ProjectId = request.ProjectId,
				Description = request.Description,
				Name = request.Name,
				EntryDate = request.EntryDate,
				UserId = request.UserId,
				Hours = hoursTotal,
			};
			var timeLog = await _repository.CreateTimeLogEntryAsync(timeLogEntry);
			return timeLog.Id;
		}
	}
}
