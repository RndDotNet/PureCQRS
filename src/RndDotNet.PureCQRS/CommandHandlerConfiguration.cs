using Microsoft.Extensions.DependencyInjection;

namespace RndDotNet.PureCQRS.Example.CQRS;

public delegate ValueTask CommandHandler<in TCommand>(TCommand command, CancellationToken ct);

public static class CommandHandlerConfiguration
{
	public static IServiceCollection AddCommandHandler<TCommand, TCommandHandler>(this IServiceCollection services) 
		where TCommandHandler : class, ICommandHandler<TCommand>
	{
		services
			.AddTransient<ICommandHandler<TCommand>, TCommandHandler>()
			.AddTransient<CommandHandler<TCommand>>(
				sp => sp.GetRequiredService<ICommandHandler<TCommand>>().Handle);

		return services;
	}
}
