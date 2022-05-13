using RndDotNet.PureCQRS.Example.CQRS;
using RndDotNet.PureCQRS.Example.Model;

namespace RndDotNet.PureCQRS.Example.Commands;

public record SendSeoCheckCommand(Post NewPost);

public sealed class SendSeoCheckCommandHandler : ICommandHandler<SendSeoCheckCommand>
{
	public async ValueTask Handle(SendSeoCheckCommand command, CancellationToken token)
	{
		// some logic here
	}
}