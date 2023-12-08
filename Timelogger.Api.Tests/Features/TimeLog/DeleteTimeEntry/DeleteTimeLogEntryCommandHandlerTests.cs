using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Threading;
using Timelogger.Features.TimeLog.DeleteTimeEntry;
using Timelogger.Repositories;

namespace Timelogger.Api.Tests.Features.TimeLog.DeleteTimeEntry
{
	public class DeleteTimeLogEntryCommandHandlerTests
	{
		[Test]
		public async Task Given_Valid_Id_Should_Delete_TimeEntry()
		{
			// Arrange
			var timeLogEntryRepositoryMock = Substitute.For<ITimeLogEntryRepository>();
			timeLogEntryRepositoryMock.DeleteTimeLogEntry(Arg.Any<int>()).Returns(true);

			// Act
			var sut = new DeleteTimeLogEntryCommandHandler(repository: timeLogEntryRepositoryMock);

			var result = await sut.Handle(new DeleteTimeLogEntryCommand()
			{
				Id = 1,
			}, CancellationToken.None);
			
			// Assert
			Assert.True(result);
		}
	}
}
