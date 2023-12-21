using System.Threading.Tasks;
using Timelogger.Entities;
using Timelogger.Exceptions;

namespace Timelogger.Repositories
{
	public interface ITimeLogEntryRepository
	{
        /// <summary>
        /// Asynchronously creates a new time log entry in the specified API context.
        /// </summary>
        /// <param name="timeLogEntry">The time log entry to be created.</param>
        /// <returns>The created time log entry.</returns>
        /// <exception cref="InvalidTimeLogEntryException">
        /// Thrown if the entity cannot be created. The exception message includes details about the error.
        /// </exception>
        Task<TimeLogEntry> CreateTimeLogEntryAsync(TimeLogEntry timeLogEntry);

        /// <summary>
        /// Asynchronously deletes a TimeLogEntry from the data context based on the specified timeEntryId.
        /// </summary>
        /// <param name="timeEntryId">The identifier of the TimeLogEntry to be deleted.</param>
        /// <returns>
        /// A task representing the asynchronous operation. 
        /// The task result is a boolean indicating whether the deletion was successful (true) or not (false).
        /// </returns>
        /// <exception cref="InvalidTimeLogEntryException">
        /// Thrown when an error occurs during the deletion process.
        /// The exception message includes details about the failure and the original exception.
        /// </exception>
        Task<bool> DeleteTimeLogEntry(int timeEntryId);
	}
}
