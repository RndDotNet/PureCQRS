using RndDotNet.PureCQRS.Example.CQRS;
using RndDotNet.PureCQRS.Example.Events;
using RndDotNet.PureCQRS.Example.Model;

namespace RndDotNet.PureCQRS.Example.Commands;

public record AddPostCommand(string Title, string Text);

public sealed class AddPostCommandHandler : ICommandHandler<AddPostCommand>
{
	private readonly IEventHandler<PostCreatedEvent> eventHandler;

	public AddPostCommandHandler(IEventHandler<PostCreatedEvent> eventHandler)
	{
		this.eventHandler = eventHandler;
	}

	public async ValueTask Handle(AddPostCommand request, CancellationToken ct)
	{
		var post = CreatePost(request);
		
		await eventHandler.Handle(new PostCreatedEvent(post), ct);
	}

	private Post CreatePost(AddPostCommand request)
	{
		// some logic here
		throw new NotImplementedException();
	}
}
