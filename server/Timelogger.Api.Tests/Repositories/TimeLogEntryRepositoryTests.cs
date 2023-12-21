using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Timelogger.Entities;
using Timelogger.Exceptions;
using Timelogger.Repositories;

namespace Timelogger.Api.Tests.Repositories
{
	[TestFixture]
	public class TimeLogEntryRepositoryTests
	{
		private ApiContext _dbContext;
		private ITimeLogEntryRepository _timeLogEntryRepository;

		[SetUp]
		public void SetUp()
		{
			_dbContext = CreateDbContext();
			_timeLogEntryRepository = new TimeLogEntryRepository(_dbContext);
		}

		[TearDown]
		public void TearDown()
		{
			_dbContext.Dispose();
		}

		[Test]
		public async Task Should_CreateTimeEntry()
		{
			// Arrange
			var timeEntry = new TimeLogEntry()
			{
				Id = 1110,
				Name = "DEV-1003 : Unit Test",
				Hours = 5,
				ProjectId = 1,
				Description = "Unit Test initiated",
				EntryDate = DateTime.Now,
				UserId = 1,
			};
			

			// Act
			var result = await _timeLogEntryRepository.CreateTimeLogEntryAsync(timeEntry);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Is.EqualTo(timeEntry));
			Assert.That(result.Id, Is.EqualTo(timeEntry.Id));
			Assert.That(result.Name, Is.EqualTo(timeEntry.Name));
			Assert.That(result.Description, Is.EqualTo(timeEntry.Description));
			Assert.That(result.Hours, Is.EqualTo(timeEntry.Hours));
			Assert.That(result.EntryDate, Is.EqualTo(timeEntry.EntryDate));
			Assert.That(result.UserId, Is.EqualTo(timeEntry.UserId));
		}

		[Test]
		public void Given_Invalid_TimeLogEntry_Should_Throw_Exception()
		{
			// Act & Assert
			Assert.ThrowsAsync<InvalidTimeLogEntryException>(async () => await _timeLogEntryRepository.CreateTimeLogEntryAsync(null));
		}

		[Test]
		public async Task ShouldReturn_Delete_TimeLogEntry()
		{
			// Act
			var result = await _timeLogEntryRepository.DeleteTimeLogEntry(1100);

			//Assert
			Assert.That(result, Is.Not.Null);
		}

		[Test]
		public Task Given_Invalid_TimeLogEntry_Id_Should_Throw_Exception()
        {
            // Arrange
            _timeLogEntryRepository = new TimeLogEntryRepository(null);
            // Act

            //Assert
            Assert.ThrowsAsync<InvalidTimeLogEntryException>(async () => await _timeLogEntryRepository.DeleteTimeLogEntry(-12));
            return Task.CompletedTask;
        }

        [Test]
        public async Task Given_TimeLogEntry_Id_Should_Return_False()
        {
            // Act
            var result = await _timeLogEntryRepository.DeleteTimeLogEntry(2000);
            //Assert
            Assert.That(result, Is.False);
        }

        private ApiContext CreateDbContext()
		{
			var options = new DbContextOptionsBuilder<ApiContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				.Options;

			var context = new ApiContext(options);
			context.Projects.AddRange(ProjectsList());
			context.SaveChangesAsync();

			return context;
		}

		private static IEnumerable<Project> ProjectsList()
		{
			var projects = new List<Project>
			{
				new()
				{
					Id = 1,
					Name = "Unit Test Project 1",
					TimeEntries =
                    [
                        new TimeLogEntry()
                        {
                            Id = 1100,
                            Name = "DEV-1003 : Unit Test",
                            Hours = 5,
                        }
                    ]
                },
				new()
				{
					Id = 2,
					Name = "Unit Test Project 1",
					TimeEntries =
                    [
                        new TimeLogEntry
                        {
                            Id = 1200,
                            Name = "DEV-1003 : Unit Test",
                            Hours = 5,
                        }
                    ]
                },
				new()
				{
					Id = 3,
					Name = "Unit Test Project 3",
					TimeEntries =
                    [
                        new TimeLogEntry()
                        {
                            Id = 1300,
                            Name = "DEV-1003 : Unit Test",
                            Hours = 5,
                        }
                    ]
                }
			};
			return projects;
		}
	}
}
