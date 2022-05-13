using RndDotNet.PureCQRS.Example.Commands;
using RndDotNet.PureCQRS.Example.CQRS;
using RndDotNet.PureCQRS.Example.Events;
using RndDotNet.PureCQRS.Example.Model;
using RndDotNet.PureCQRS.Example.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddQueryHandler<GetListPostsQuery, List<Post>, GetListPostsQueryHandler>();
builder.Services.AddEventHandler<PostCreatedEvent, PostCreatedEventHandler>();
builder.Services.AddCommandHandler<AddPostCommand, AddPostCommandHandler>();
builder.Services.AddCommandHandler<SendSeoCheckCommand, SendSeoCheckCommandHandler>();
builder.Services.AddCommandHandler<SendToGrammarCheckerCommand, SendToGrammarCheckerCommandHandler>();

var app = builder.Build();

app.MapGet("/post/list", HandleGetListPosts)
	.Produces(StatusCodes.Status200OK)
	.Produces(StatusCodes.Status400BadRequest)
	.Produces(StatusCodes.Status404NotFound);

app.MapPost("/post/", HandleAddPost)
	.Produces(StatusCodes.Status200OK)
	.Produces(StatusCodes.Status400BadRequest);

app.Run();

async ValueTask<IResult> HandleGetListPosts(
	QueryHandler<GetListPostsQuery, List<Post>> handler,
	int? limit,
	int? offset,
	CancellationToken ct)
{
	var result = await handler(new GetListPostsQuery(limit, offset), ct);
	return result switch
	{
		{ Count: 0 } => Results.NotFound(),
		_ => Results.Ok(result)
	};
}

async ValueTask<IResult> HandleAddPost(
	CommandHandler<AddPostCommand> handler,
	AddPostCommand request,
	CancellationToken ct)
{
	await handler(request, ct);
	return Results.Ok();
}
