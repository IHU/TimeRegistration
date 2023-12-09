using System;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Timelogger.Repositories;

namespace Timelogger.Features.Projects.GetAllProjects
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, IOrderedEnumerable<ProjectResponse>>
    {
        private readonly IProjectRepository _repository;

        public GetAllProjectsQueryHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<IOrderedEnumerable<ProjectResponse>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _repository.GetAllProjectsAsync();

            return projects.Select(p => new ProjectResponse()
            {
                Id = p.Id,
                Name = p.Name,
                Notes = p.Notes,
                UserId = p.UserId,
                CustomerId = p.CustomerId,
                EndDate = p.EndDate.ToShortDateString(),
                IsCompleted = p.EndDate < DateTime.Now,
                HoursSpend = p.TimeEntries?.Sum(t => t.Hours) ?? 0,

            }).ToList().OrderBy(d => d.EndDate);
        }
    }
}
