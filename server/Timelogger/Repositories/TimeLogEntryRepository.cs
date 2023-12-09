using System;
using System.Threading.Tasks;
using Timelogger.Entities;
using Timelogger.Exceptions;

namespace Timelogger.Repositories
{
	public class TimeLogEntryRepository : ITimeLogEntryRepository
	{
		private readonly ApiContext _apiContext;

		public TimeLogEntryRepository(ApiContext apiContext)
		{
			_apiContext = apiContext;
		}

		public async Task<TimeLogEntry> CreateTimeLogEntryAsync(TimeLogEntry timeLogEntry)
		{
			try
			{
				await _apiContext.TimeEntries.AddAsync(timeLogEntry);
				await _apiContext.SaveChangesAsync();

				return timeLogEntry;
			}
			catch (Exception ex)
			{
				throw new InvalidTimeLogEntryException("Entity can not be created." + ex.Message, ex);
			}
		}

		public async Task<bool> DeleteTimeLogEntry(int timeEntryId)
		{
			try
			{
				var entryFound = _apiContext.Find<TimeLogEntry>(timeEntryId);
				_apiContext.Remove(entryFound);
				await _apiContext.SaveChangesAsync();
				return true;
			}
			catch (Exception e)
			{
				throw new InvalidTimeLogEntryException($"Entity with id {timeEntryId} Could not deleted." + e.Message, e);
			}
		}
	}
}
