using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Timelogger.Entities;
using Timelogger.Features.Projects.GetProject;
using Timelogger.Repositories;

namespace Timelogger.Api.Tests.Features.Projects.GetProject
{
    public class GetProjectByIdQueryHandlerTests
	{
        [TestCase(1)]
        [TestCase(2)]
        public async Task GivenValid_ProjectId_Should_Get_Project(int id)
        {

            var projectRepositoryMock = Substitute.For<IProjectRepository>();
            projectRepositoryMock.GetProjectAsync(Arg.Any<int>()).Returns(new Project() { Id = 1 });

            var sut = new GetProjectByIdQueryHandler(repository: projectRepositoryMock);

            var result = await sut.Handle(new GetProjectByIdQuery() { Id = id }, CancellationToken.None);

            Assert.That(result, Is.Not.Null);

        }

        [Test]
        public async Task Given_Invalid_ProjectId_Should_Return_Null()
        {

            var projectRepositoryMock = Substitute.For<IProjectRepository>();
            projectRepositoryMock.GetProjectAsync(Arg.Any<int>()).Returns(new Project() { Id = 1 });

            var sut = new GetProjectByIdQueryHandler(repository: projectRepositoryMock);

            var result = await sut.Handle(null, CancellationToken.None);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task Given_Valid_ProjectId_Should_Return_Null()
        {

	        var projectRepositoryMock = Substitute.For<IProjectRepository>();
	        var sut = new GetProjectByIdQueryHandler(repository: projectRepositoryMock);

	        var result = await sut.Handle(
		        new GetProjectByIdQuery()
		        {
			        Id = 12,
		        }, CancellationToken.None);

	        Assert.That(result, Is.Null);
        }
	}
}
