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
	public class ProjectRepositoryTests
	{
		private ApiContext _dbContext;
		private IProjectRepository _projectRepository;

		[SetUp]
		public void SetUp()
		{
			_dbContext = CreateDbContext();
			_projectRepository = new ProjectRepository(_dbContext);
		}

		[TearDown]
		public void TearDown()
		{
			_dbContext.Dispose();
		}

		[Test]
		public async Task ShouldReturn_ProjectById()
		{
			// Arrange
			var expectedProject = new Project()
			{
				Id = 1,
				Name = "Unit Test Project 1",
				UserId = 1,
				TimeEntries = new List<TimeLogEntry>()
				{
					new TimeLogEntry()
					{
						Id = 1100,
						Name = "DEV-1003 : Unit Test",
						Hours = 5,
					}
				}
			};

			// Act
			var result = await _projectRepository.GetProjectAsync(1);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result.Id, Is.EqualTo(expectedProject.Id));
			Assert.That(result.Name, Is.EqualTo(expectedProject.Name));
			Assert.That(result.Notes, Is.EqualTo(expectedProject.Notes));
			Assert.That(result.UserId, Is.EqualTo(expectedProject.UserId));
			Assert.That(result.EndDate, Is.EqualTo(expectedProject.EndDate));
			Assert.That(result.TimeEntries.Count, Is.EqualTo(expectedProject.TimeEntries.Count));
		}

		[Test]
		public void Given_ProjectId_Should_Throw_InvalidProjectException()
		{
			// Act & Assert
			var invalidId = 1250;
			Assert.ThrowsAsync<InvalidProjectException>(async () => await _projectRepository.GetProjectAsync(invalidId));
		}

		[Test]
		public async Task ShouldReturn_AllProjects()
		{
			// Act
			var result = await _projectRepository.GetAllProjectsAsync();

			//Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result.Count, Is.EqualTo(3));
		}

		[Test]
		public async Task Should_Return_Empty()
		{
			// Arrange
			var options = new DbContextOptionsBuilder<ApiContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				.Options;

			var context = new ApiContext(options);

			// Act
			var sut = new ProjectRepository(context);
			var result = await sut.GetAllProjectsAsync();

			//  Assert
			Assert.That(result, Is.Not.Null);

		}

		[Test]
		public async Task ShouldReturn_Create_Project()
		{
			// Act
			var project = new Project()
			{
				Name = "Unit Test Project 11",
				TimeEntries = new List<TimeLogEntry>()
					{
						new TimeLogEntry()
						{
							Id = 1110,
							Name = "DEV-1003 : Unit Test",
							Hours = 5,
						}
					}
			};

			var result = await _projectRepository.CreateProjectAsync(project);

			//Assert
			Assert.That(result, Is.EqualTo(4));
		}

		[Test]
		public void Should_Create_Project_Throw_InvalidProjectException()
		{
			//Assert
			Assert.ThrowsAsync<InvalidProjectException>(async () => await _projectRepository.CreateProjectAsync(null));
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

		private IEnumerable<Project> ProjectsList()
		{
			var projects = new List<Project>
			{
				new Project(id: 1, name: "Unit Test Project 1", userId: 1, timeEntries:
				[
					new TimeLogEntry(id: 1100, name: "DEV-1003 : Unit Test", hours: 5)
				]),
				new Project(id: 2, name: "Unit Test Project 1", timeEntries:
				[
					new TimeLogEntry(id: 1200, name: "DEV-1003 : Unit Test", hours: 5)
				]),
				new Project(id: 3, name: "Unit Test Project 3", timeEntries:
				[
					new TimeLogEntry(id: 1300, name: "DEV-1003 : Unit Test", hours: 5)
				])
			};
			return projects;
		}
	}
}
