using Microsoft.Extensions.DependencyInjection;

namespace Timelogger
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddTimeLogger(this IServiceCollection services)
		{
			// Add MediatR with handlers from the Application assembly
			services.AddMediatR(configuration =>
			{
				configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
			});
			return services;
		}
	}
}
