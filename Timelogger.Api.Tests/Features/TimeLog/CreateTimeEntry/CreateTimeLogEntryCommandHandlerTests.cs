using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using System.Threading;
using Timelogger.Entities;
using Timelogger.Features.TimeLog.CreateTimeEntry;
using Timelogger.Repositories;

namespace Timelogger.Api.Tests.Features.TimeLog.CreateTimeEntry
{
	public class CreateTimeLogEntryCommandHandlerTests
	{
		[Test]
		public async Task Should_Create_TimeEntry()
		{

			var timeLogEntryRepositoryMock = Substitute.For<ITimeLogEntryRepository>();
			timeLogEntryRepositoryMock.CreateTimeLogEntryAsync(Arg.Any<TimeLogEntry>()).Returns(new TimeLogEntry() { Id = 1 });

			var sut = new CreateTimeLogEntryCommandHandler(repository: timeLogEntryRepositoryMock);

			var result = await sut.Handle(new CreateTimeLogEntryCommand()
			{
				Name = "DEV-1022 : Unit test",
				Description = "Unit test started.",
				EntryDate = DateTime.Now,
				Hours = "5.30",
				ProjectId = 1,
				UserId = 2,
			}, CancellationToken.None);

			Assert.IsNotNull(result);
			Assert.AreEqual(1,result);

		}
	}
}
