using MediatR;

namespace Timelogger.Features.Projects.GetProject
{
    public class GetProjectByIdQuery : IRequest<ProjectResponse>
    {
        public int Id { get; set; }
    }
}
