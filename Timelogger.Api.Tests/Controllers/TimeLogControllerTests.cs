using MediatR;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Timelogger.Api.Controllers;
using Timelogger.Features.TimeLog.CreateTimeEntry;
using Timelogger.Features.TimeLog.DeleteTimeEntry;

namespace Timelogger.Api.Tests.Controllers
{
	public class TimeLogControllerTests
	{
		[Test]
		public async Task Create_TimeEntry_GivenValidProjectId_ToProject()
		{
			var sut = GetSystemUnderTest();

			var result = await sut.CreateTimeEntry(new CreateTimeLogEntryCommand()
			{
				Name = "Dev-1022: Baseline Created",
				Hours = 3,
				Description = "Description for the task",
				EntryDate = DateTime.Now,
				ProjectId = 1,
				UserId = 12,
			});

			Assert.That(result, Is.Not.Null);
			
		}

		[Test]
		public async Task Delete_TimeEntry_GivenValidProjectId_FromProject()
		{
			var sut = GetSystemUnderTest();

			var result = await sut.Delete(1);

			Assert.That(result, Is.Not.Null);
		}
		private TimeLogController GetSystemUnderTest()
		{
			var mediatorMock = Substitute.For<IMediator>();
			mediatorMock.Send(Arg.Any<CreateTimeLogEntryCommand>()).Returns(1);
			mediatorMock.Send(Arg.Any<DeleteTimeLogEntryCommand>()).Returns(true);

			return new TimeLogController(mediatorMock)
			{
				ControllerContext = {
					HttpContext = new DefaultHttpContext()
				}
			};
		}
	}
}
