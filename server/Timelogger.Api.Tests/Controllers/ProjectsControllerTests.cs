using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Timelogger.Api.Controllers;
using NUnit.Framework;
using NSubstitute;
using Timelogger.Entities;
using Timelogger.Features.Projects;
using Microsoft.AspNetCore.Mvc;
using Timelogger.Features.Projects.GetAllProjects;
using Timelogger.Features.Projects.GetProject;

namespace Timelogger.Api.Tests.Controllers
{
    public class ProjectsControllerTests
    {
        [Test]
        public async Task Given_Id_ShouldGet_Project()
        {
            var sut = GetSystemUnderTest();

            var result = await sut.GetProjectAsync(1);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var responseResult = (result as OkObjectResult)?.Value as ProjectResponse;
            Assert.That(responseResult?.Id, Is.EqualTo(1));
            Assert.That(responseResult.TimeEntries, Is.Not.Null);
        }

        [Test]
        public async Task ShouldGet_All_Projects()
        {
	        var sut = GetSystemUnderTest();

	        var result = await sut.GetAllAsync();
	        Assert.That(result, Is.Not.Null);
	        Assert.That(result, Is.InstanceOf<OkObjectResult>());
	        var responseResult = (result as OkObjectResult)?.Value as IOrderedEnumerable<ProjectResponse>;
	        Assert.That(responseResult, Is.Not.Null);
	        Assert.That(responseResult.Count(), Is.EqualTo(1));
        }

		private ProjectsController GetSystemUnderTest()
		{
			var project = new ProjectResponse()
			{
				Id = 1,
				Name = "Project 1",
				HoursSpend = 10,
				Notes = "Notes about project 1",
				TimeEntries = new List<TimeLogEntry>()
				{
					new TimeLogEntry()
					{
						Hours = 10,
						Id = 1,
						Name = "DEV-1001 : Project Test",
						ProjectId = 1,
						Description = "Project test is in progress",
						EntryDate = DateTime.Now,
					}
				}
			};
			var projects = new List<ProjectResponse>()
			{
				project,
			}.OrderBy(p=>p.EndDate);

			var mediatorMock = Substitute.For<IMediator>();
            mediatorMock.Send(Arg.Any<GetProjectByIdQuery>()).Returns(callInfo => project);
			mediatorMock.Send(Arg.Any<GetAllProjectsQuery>()).Returns(callInfo => projects);

			return new ProjectsController(mediatorMock)
            {
                ControllerContext = {
                    HttpContext = new DefaultHttpContext()
                }
            };
        }
    }
}
