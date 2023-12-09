using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Timelogger.Features.Projects.GetAllProjects;
using Timelogger.Features.Projects.GetProject;

namespace Timelogger.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProjectsController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ProjectsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			var result = await _mediator.Send(new GetAllProjectsQuery());
			return Ok(result);
		}

		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetProjectAsync(int id)	
		{
			var result = await _mediator.Send(
				new GetProjectByIdQuery()
				{
					Id = id,
				});
			return Ok(result);
		}
	}
}
