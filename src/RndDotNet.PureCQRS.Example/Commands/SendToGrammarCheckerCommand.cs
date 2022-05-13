using RndDotNet.PureCQRS.Example.CQRS;
using RndDotNet.PureCQRS.Example.Model;

namespace RndDotNet.PureCQRS.Example.Commands;

public record SendToGrammarCheckerCommand(Post NewPost);

public sealed class SendToGrammarCheckerCommandHandler : ICommandHandler<SendToGrammarCheckerCommand>
{
	public async ValueTask Handle(SendToGrammarCheckerCommand command, CancellationToken token)
	{
		// some logic here
	}
}
