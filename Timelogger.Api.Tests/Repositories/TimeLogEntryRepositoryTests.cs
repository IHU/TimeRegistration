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
			Assert.IsNotNull(result);
			Assert.AreEqual(timeEntry, result);
			Assert.AreEqual(timeEntry.Id, result.Id);
			Assert.AreEqual(timeEntry.Name, result.Name);
			Assert.AreEqual(timeEntry.Description, result.Description);
			Assert.AreEqual(timeEntry.Hours, result.Hours);
			Assert.AreEqual(timeEntry.EntryDate, result.EntryDate);
			Assert.AreEqual(timeEntry.UserId, result.UserId);
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
			Assert.True(result);
		}

		[Test]
		public async Task Given_Invalid_TimeLogEntry_Id_Should_Throw_Exception()
		{
			// Act
			//Assert
			Assert.ThrowsAsync<InvalidTimeLogEntryException>(async () => await _timeLogEntryRepository.DeleteTimeLogEntry(1000));
		}

		//[Test]
		//public async Task Should_Return_Empty()
		//{
		//	// Arrange
		//	var options = new DbContextOptionsBuilder<ApiContext>()
		//		.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
		//		.Options;

		//	var context = new ApiContext(options);

		//	// Act
		//	var sut = new ProjectRepository(context);
		//	var result = await sut.GetAllProjectsAsync();

		//	//  Assert
		//	Assert.IsEmpty(result);

		//}

		//[Test]
		//public async Task ShouldReturn_Create_Project()
		//{
		//	// Act
		//	var project = new Project()
		//	{
		//		Name = "Unit Test Project 11",
		//		TimeEntries = new List<TimeLogEntry>()
		//			{
		//				new TimeLogEntry()
		//				{
		//					Id = 1110,
		//					Name = "DEV-1003 : Unit Test",
		//					Hours = 5,
		//				}
		//			}
		//	};

		//	var result = await _timeLogEntryRepository.CreateProjectAsync(project);

		//	//Assert
		//	Assert.AreEqual(4,result);
		//}

		//[Test]
		//public void Should_Create_Project_Throw_InvalidProjectException()
		//{
		//	//Assert
		//	Assert.ThrowsAsync<InvalidProjectException>(async () => await _timeLogEntryRepository.CreateProjectAsync(null));
		//}


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

		private IEnumerable<Project> ProjectsList()
		{
			var projects = new List<Project>
			{
				new Project()
				{
					Id = 1,
					Name = "Unit Test Project 1",
					TimeEntries = new List<TimeLogEntry>()
					{
						new TimeLogEntry()
						{
							Id = 1100,
							Name = "DEV-1003 : Unit Test",
							Hours = 5,
						}
					}
				},
				new Project()
				{
					Id = 2,
					Name = "Unit Test Project 1",
					TimeEntries = new List<TimeLogEntry>()
					{
						new TimeLogEntry()
						{
							Id = 1200,
							Name = "DEV-1003 : Unit Test",
							Hours = 5,
						}
					}
				},
				new Project()
				{
					Id = 3,
					Name = "Unit Test Project 3",
					TimeEntries = new List<TimeLogEntry>()
					{
						new TimeLogEntry()
						{
							Id = 1300,
							Name = "DEV-1003 : Unit Test",
							Hours = 5,
						}
					}
				}
			};
			return projects;
		}
	}
}
