using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Timelogger.Features.TimeLog.CreateTimeEntry;
using Timelogger.Features.TimeLog.DeleteTimeEntry;

namespace Timelogger.Api.Controllers
{
    [Route("api/[controller]")]
	public class TimeLogController : ControllerBase
	{
		private readonly IMediator _mediator;

		public TimeLogController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> CreateTimeEntry([FromBody] CreateTimeLogEntryCommand command)
		{
			var result = await _mediator.Send(command); 
			return Ok(result);
		}

		[HttpDelete]
		[Route("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await _mediator.Send(new DeleteTimeLogEntryCommand()
			{
				Id = id,
			} );
			return Ok(result);
		}
	}
}
