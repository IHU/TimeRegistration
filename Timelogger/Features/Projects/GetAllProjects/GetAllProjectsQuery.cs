using System.Linq;
using MediatR;

namespace Timelogger.Features.Projects.GetAllProjects
{
    public class GetAllProjectsQuery : IRequest<IOrderedEnumerable<ProjectResponse>>
    {
    }
}
