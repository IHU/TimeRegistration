using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timelogger.Entities;
using Timelogger.Exceptions;
namespace Timelogger.Repositories
{
	public class ProjectRepository : IProjectRepository
	{
		private readonly ApiContext _context;

		public ProjectRepository(ApiContext context)
		{
			_context = context;
		}

		public async Task<Project> GetProjectAsync(int id)
		{
			var project = await _context.Projects
				.Include(e => e.TimeEntries)
				.FirstOrDefaultAsync(p => p.Id == id);

			return project ?? throw new InvalidProjectException("Project not found");
		}

		public async Task<List<Project>> GetAllProjectsAsync()
		{
			var projects = await _context.Projects
											.Include(e => e.TimeEntries)
											.ToListAsync();

			return projects ?? throw new InvalidProjectException("No Project Found.");
		}

		public async Task<int> CreateProjectAsync(Project project)
		{
			try
			{
				await _context.Projects.AddAsync(project);
				await _context.SaveChangesAsync();
				return project.Id;
			}
			catch (Exception e)
			{
				throw new InvalidProjectException("Project can not be created.", e);
			}
		}
	}
}
