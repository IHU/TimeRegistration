using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Timelogger.Repositories;

namespace Timelogger.Features.Projects.GetProject
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectResponse>
    {
        private readonly IProjectRepository _repository;

        public GetProjectByIdQueryHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProjectResponse> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
	        if (request == null)
	        {
		        return null;
	        }

			var project = await _repository.GetProjectAsync(request.Id);
            
			if (project == null)
            {
                return null;
            }

            return new ProjectResponse()
            {
                Id = project.Id,
                Name = project.Name,
                CustomerId = project.CustomerId,
                UserId = project.UserId,
                Notes = project.Notes,
                EndDate = project.EndDate.ToShortDateString(),
                HoursSpend = project.TimeEntries?.Sum(t => t.Hours) ?? 0,
                IsCompleted = project.EndDate < DateTime.Now,
                TimeEntries = project.TimeEntries,
            };
        }
    }
}
