using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Timelogger.Entities;
using System;
using Microsoft.OpenApi.Models;
using Timelogger.Repositories;

namespace Timelogger.Api
{
	public class Startup
	{
		private readonly IWebHostEnvironment _environment;
		public IConfigurationRoot Configuration { get; }

		public Startup(IWebHostEnvironment env)
		{
			_environment = env;

			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddTimeLogger();

			services.AddScoped<IProjectRepository, ProjectRepository>();
			services.AddScoped<ITimeLogEntryRepository, TimeLogEntryRepository>();


			// Add framework services.
			services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("e-conomic interview"));
			services.AddLogging(builder =>
			{
				builder.AddConsole();
				builder.AddDebug();
			});

			services.AddMvc(options => options.EnableEndpointRouting = false);

			services.AddSwaggerGen();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "e-conomic Consultant Timesheet API",
					Description = "Welcome to the e-conomic API documentation!",
				});
			});

			if (_environment.IsDevelopment())
			{
				services.AddCors();
			}
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseCors(builder => builder
					.AllowAnyMethod()
					.AllowAnyHeader()
					.SetIsOriginAllowed(origin => true)
					.AllowCredentials());
			}

			app.UseMvc();

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "e-conomic TimeLogger API V1");
			});

			var serviceScopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();

			using var scope = serviceScopeFactory.CreateScope();

			SeedDatabase(scope);
		}

		private static void SeedDatabase(IServiceScope scope)
		{
			var context = scope.ServiceProvider.GetService<ApiContext>();

			context.Projects.AddRange(
			new Project
			{
				Id = 1,
				Name = "Project A",
				EndDate = new DateTime(2024, 1, 1),
				CustomerId = 20001,
				IsFinished = false,
				Notes = String.Empty,
				UserId = 30001
			},
			new Project
			{
				Id = 2,
				Name = "Project B",
				EndDate = new DateTime(2024, 2, 1),
				CustomerId = 20002,
				IsFinished = false,
				Notes = String.Empty,
				UserId = 30001,
			},
			new Project
			{
				Id = 3,
				Name = "Project C",
				EndDate = new DateTime(2024, 3, 1),
				CustomerId = 20003,
				IsFinished = true,
				Notes = String.Empty,
				UserId = 30001
			},
			new Project
			{
				Id = 4,
				Name = "Project D",
				EndDate = new DateTime(2024, 4, 1),
				CustomerId = 20003,
				IsFinished = true,
				Notes = String.Empty,
				UserId = 30001
			},
			new Project
			{
				Id = 5,
				Name = "Project E",
				EndDate = new DateTime(2023, 10, 1),
				CustomerId = 20003,
				IsFinished = true,
				Notes = String.Empty,
				UserId = 30001
			}
			);

			context.TimeEntries.AddRange(
			new TimeLogEntry
			{
				Id = 10,
				ProjectId = 2,
				Name = "DEV4001 : Api end point fixed",
				Description = "Description : Api end point fixed",
				EntryDate = DateTime.Today,
				UserId = 30001,
				Hours = 12,
			},
			new TimeLogEntry
			{
				Id = 11,
				ProjectId = 2,
				Name = "DEV4003 : Validation to input form added",
				Description = "Description : Validation to input form added",
				EntryDate = DateTime.Today,
				UserId = 30001,
				Hours = 4,
			},
			new TimeLogEntry
			{
				Id = 12,
				ProjectId = 3,
				Name = "DEV5001 : Project Done!",
				Description = "Description : Project Done!",
				EntryDate = DateTime.Today,
				UserId = 30001,
				Hours = 150,
			});
			context.SaveChanges();
		}
	}
}