using Microsoft.Extensions.DependencyInjection;

namespace RndDotNet.PureCQRS.Example.CQRS;

public delegate ValueTask EventHandler<in TEvent>(TEvent @event, CancellationToken ct);

public static class EventHandlerConfiguration
{
	public static IServiceCollection AddEventHandler<TEvent, TEventHandler>(this IServiceCollection services)
		where TEventHandler : class, IEventHandler<TEvent>
	{
		services
			.AddTransient<IEventHandler<TEvent>, TEventHandler>()
			.AddTransient<EventHandler<TEvent>>(
				sp => sp.GetRequiredService<IEventHandler<TEvent>>().Handle);

		return services;
	}
}