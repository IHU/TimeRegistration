using System;
using System.Threading.Tasks;
using Timelogger.Entities;
using Timelogger.Exceptions;

namespace Timelogger.Repositories
{
	public class TimeLogEntryRepository(ApiContext apiContext) : ITimeLogEntryRepository
    {
        public async Task<TimeLogEntry> CreateTimeLogEntryAsync(TimeLogEntry timeLogEntry)
		{
			try
			{
				await apiContext.TimeEntries.AddAsync(timeLogEntry);
				
                await apiContext.SaveChangesAsync();

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
                // Find the TimeLogEntry by its identifier
                var entryFound = apiContext.Find<TimeLogEntry>(timeEntryId);

                if (entryFound == null)
                {
                    return false;
                }

                apiContext.Remove(entryFound);
				
                await apiContext.SaveChangesAsync();

				return true;
			}
			catch (Exception e)
			{
				throw new InvalidTimeLogEntryException($"Entity with id {timeEntryId} Could not deleted." + e.Message, e);
			}
		}
	}
}
