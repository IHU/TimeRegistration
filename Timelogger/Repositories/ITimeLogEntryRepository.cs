using System.Threading.Tasks;
using Timelogger.Entities;

namespace Timelogger.Repositories
{
	public interface ITimeLogEntryRepository
	{
		Task<TimeLogEntry> CreateTimeLogEntryAsync(TimeLogEntry timeEntry);
		Task<bool> DeleteTimeLogEntry(int timeEntryId);
	}
}
