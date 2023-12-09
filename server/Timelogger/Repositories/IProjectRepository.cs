using System.Collections.Generic;
using System.Threading.Tasks;
using Timelogger.Entities;

namespace Timelogger.Repositories
{
    public interface IProjectRepository
    {
        Task<Project> GetProjectAsync(int id);
        Task<List<Project>> GetAllProjectsAsync();
        Task<int> CreateProjectAsync(Project project);
    }
}