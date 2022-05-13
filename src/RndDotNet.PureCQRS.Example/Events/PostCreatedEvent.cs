﻿using RndDotNet.PureCQRS.Example.Commands;
using RndDotNet.PureCQRS.Example.CQRS;
using RndDotNet.PureCQRS.Example.Model;

namespace RndDotNet.PureCQRS.Example.Events;

public record PostCreatedEvent(Post CreatedPost);

public sealed class PostCreatedEventHandler : IEventHandler<PostCreatedEvent>
{
	private readonly ICommandHandler<SendToGrammarCheckerCommand> sendToGrammarCheckerCommand;

	private readonly ICommandHandler<SendSeoCheckCommand> sendSeoCheckCommand;

	public PostCreatedEventHandler(ICommandHandler<SendToGrammarCheckerCommand> sendToGrammarCheckerCommand,
		ICommandHandler<SendSeoCheckCommand> sendSeoCheckCommand)
	{
		this.sendToGrammarCheckerCommand = sendToGrammarCheckerCommand;
		this.sendSeoCheckCommand = sendSeoCheckCommand;
	}

	public async ValueTask Handle(PostCreatedEvent @event, CancellationToken token)
	{
		var sendToGrammarCheckerCommandTask = sendToGrammarCheckerCommand.Handle(new SendToGrammarCheckerCommand(@event.CreatedPost), token);
		var sendSeoCheckCommandTask = sendSeoCheckCommand.Handle(new SendSeoCheckCommand(@event.CreatedPost), token);

		await sendToGrammarCheckerCommandTask;
		await sendSeoCheckCommandTask;
	}
}
