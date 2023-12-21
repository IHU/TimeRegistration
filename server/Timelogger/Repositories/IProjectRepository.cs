using System.Collections.Generic;
using System.Threading.Tasks;
using Timelogger.Entities;
using Timelogger.Exceptions;

namespace Timelogger.Repositories
{
    /// <summary>
    /// Represents a repository for managing Project entities in the database.
    /// </summary>
    public interface IProjectRepository
    {
        /// <summary>
        /// Asynchronously retrieves a project with the specified project id, including its associated time entries.
        /// </summary>
        /// <param name="id">The unique identifier of the project to retrieve.</param>
        /// <returns>
        /// The task result is the retrieved project.
        /// If no project is found with the specified project id, a <see cref="InvalidProjectException"/> is thrown.
        /// </returns>
        Task<Project> GetProjectAsync(int id);

        /// <summary>
        /// Retrieves all projects asynchronously, including associated time entries, from the data store.
        /// </summary>
        /// <returns>
        /// The task result is a list of Project entities.
        /// If no project found, empty list will be returned.
        /// </returns>
        Task<List<Project>> GetAllProjectsAsync();

        /// <summary>
        /// Asynchronously creates a new project and adds it to the database.
        /// </summary>
        /// <param name="project">The <see cref="Project"/> entity to be created and added.</param>
        /// <returns>
        /// The task result is the ID of the created project.
        /// </returns>
        /// <exception cref="InvalidProjectException">
        /// Thrown if the project cannot be created. See inner exception for details.
        /// </exception>
        Task<int> CreateProjectAsync(Project project);
    }
}