using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Timelogger.Entities;
using Timelogger.Features.Projects.GetAllProjects;
using Timelogger.Repositories;

namespace Timelogger.Api.Tests.Features.Projects.GetAllProjects
{
	public class GetAllProjectsQueryHandlerTests
	{
		[Test]
		public async Task Should_GetAllProjects()
		{
			var projects = new List<Project>()
			{
				new Project() { Id = 1 },
				new Project() { Id = 2 },
				new Project() { Id = 3 },
				new Project() { Id = 4 },
			};

			var projectRepositoryMock = Substitute.For<IProjectRepository>();
			projectRepositoryMock.GetAllProjectsAsync().Returns(projects);

			var sut = new GetAllProjectsQueryHandler(projectRepositoryMock);

			var result = await sut.Handle(new GetAllProjectsQuery(), CancellationToken.None);


			Assert.IsNotNull(result);

		}
	}
}
