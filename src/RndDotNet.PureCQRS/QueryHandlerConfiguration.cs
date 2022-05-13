using Microsoft.Extensions.DependencyInjection;

namespace RndDotNet.PureCQRS.Example.CQRS;

public delegate ValueTask<TResult> QueryHandler<in TQuery, TResult>(TQuery query, CancellationToken ct)
	where TQuery : IQuery<TResult>;

public static class QueryHandlerConfiguration
{
	public static IServiceCollection AddQueryHandler<TQuery, TResult, TQueryHandler>(this IServiceCollection services)
		where TQueryHandler : class, IQueryHandler<TQuery, TResult> 
		where TQuery : IQuery<TResult>
	{
		services
			.AddTransient<IQueryHandler<TQuery, TResult>, TQueryHandler>()
			.AddTransient<QueryHandler<TQuery, TResult>>(
				sp => sp.GetRequiredService<IQueryHandler<TQuery, TResult>>().Handle);

		return services;
	}
}