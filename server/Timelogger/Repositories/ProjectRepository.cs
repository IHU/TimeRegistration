using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timelogger.Entities;
using Timelogger.Exceptions;

namespace Timelogger.Repositories
{
    /// <summary>
    /// Represents a repository for managing Project entities in the database.
    /// Implements the <see cref="IProjectRepository"/> interface.
    /// Initializes a new instance of the <see cref="ProjectRepository"/> class.
    /// <param name="context">The database context.</param>
    /// </summary>
    public class ProjectRepository(ApiContext context) : IProjectRepository
    {
        public async Task<Project> GetProjectAsync(int id)
		{
			var project = await context.Projects
				.Include(e => e.TimeEntries)
				.FirstOrDefaultAsync(p => p.Id == id);

			return project ?? throw new InvalidProjectException("Project not found");
		}

		public async Task<List<Project>> GetAllProjectsAsync()
		{
            try
            {
                // Use Include to eagerly load the TimeEntries related to each project
                var projects = await context.Projects
                    .Include(project => project.TimeEntries)
                    .ToListAsync();

                return projects;
            }
            catch (Exception ex)
            {
                throw new InvalidProjectException("Error occurred while retrieving projects.", ex);
            }
        }

		public async Task<int> CreateProjectAsync(Project project)
		{
			try
			{
				await context.Projects.AddAsync(project);

				await context.SaveChangesAsync();
				
                return project.Id;
			}
			catch (Exception e)
			{
				throw new InvalidProjectException("Project can not be created.", e);
			}
		}
	}
}
