using Microsoft.EntityFrameworkCore;
using Timelogger.Entities;

namespace Timelogger
{
    /// <summary>
    /// Represents the database context for the API, providing access to tables such as Projects and TimeEntries.
    /// Initializes a new instance of the <see cref="ApiContext"/> class with the specified options.
    /// </summary>
    /// <param name="options">The options for configuring the context.</param>
    public class ApiContext(DbContextOptions<ApiContext> options) : DbContext(options)
    {
        /// <summary>
        /// Gets or sets the DbSet representing the collection of Project entities in the database.
        /// </summary>
        public DbSet<Project> Projects { get; set; }

        /// <summary>
        /// Gets or sets the DbSet representing the collection of TimeLogEntry entities in the database.
        /// </summary>
        public DbSet<TimeLogEntry> TimeEntries { get; set; }
	}

}
