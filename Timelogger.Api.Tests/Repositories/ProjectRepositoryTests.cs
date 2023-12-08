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
			Assert.IsNotNull(result);
			Assert.AreEqual(expectedProject.Id, result.Id);
			Assert.AreEqual(expectedProject.Name, result.Name);
			Assert.AreEqual(expectedProject.TimeEntries.Count, result.TimeEntries.Count);
		}

		[Test]
		public void Given_ProjectId_Should_Throw_InvalidProjectException()
		{
			// Act & Assert
			var invalidId = -1;
			Assert.ThrowsAsync<InvalidProjectException>(async () => await _projectRepository.GetProjectAsync(invalidId));
		}

		[Test]
		public async Task ShouldReturn_AllProjects()
		{
			// Act
			var result = await _projectRepository.GetAllProjectsAsync();

			//Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count);
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
			Assert.IsEmpty(result);

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
			Assert.AreEqual(4,result);
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
