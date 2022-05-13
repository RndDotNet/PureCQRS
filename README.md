# PureCQRS

![GitHub Workflow Status](https://img.shields.io/github/workflow/status/RndDotNet/PureCQRS/Release%20to%20NuGet)
![GitHub release (latest by date including pre-releases)](https://img.shields.io/github/v/release/RndDotNet/PureCQRS?include_prereleases)

PureCQRS is an example of CQRS infrastructure for ASP.NET and Minimal APIs. 

you can try **PureCQRS** for yourself by using the package [`RndDotNet.PureCQRS`](https://www.nuget.org/packages/RndDotNet.PureCQRS/).

## Using

First add package `RndDotNet.PureCQRS`:

```
dotnet add package RndDotNet.PureCQRS
```

Then you may write your first query and handler. Query handlers must be inherited from `IQueryHandler<in TQuery, TResult>`:

```csharp
public record SampleQuery(int Param);

public sealed class SampleQueryHandler : IQueryHandler<SampleQuery, SampleResult>
{
	public async ValueTask<SampleResult> Handle(SampleQuery query, CancellationToken ct)
	{
		// add logic here
	}
}
```

Similarly, use `CommandHandler<in TCommand>` interface for command handlers:

```csharp
public record SampleCommand(string Title, string Text);

public sealed class SampleCommandHandler : ICommandHandler<SampleCommand>
{ 
	public async ValueTask Handle(SampleCommand request, CancellationToken ct)
	{
		// add logic here
	}
}
```

... and `IEventHandler<in T>` for event handlers:

```csharp
public record SampleEvent(SampleResult CreatedSampleResult);

public sealed class SampleEventHandler : IEventHandler<SampleEvent>
{
	public async ValueTask Handle(SampleEvent @event, CancellationToken token)
	{
		// add logic here
	}
}
```

There are special methods for registration handlers in DI-container:
```csharp
builder.Services.AddQueryHandler<SampleQuery, SampleResult, SampleQueryHandler>();
builder.Services.AddCommandHandler<SampleCommand, SampleCommandHandler>();
builder.Services.AddEventHandler<SampleEvent, SampleEventHandler>();
```

After that you can use handlers in DI for inject them to the controller, controller's method or action method in Minimal API:

```csharp
app.MapGet("/",
	async (QueryHandler<SampleQuery, SampleResult> handler, int parameter, CancellationToken ct) =>
	{
		var result = await handler(new SampleQuery(parameter), ct);
		return Results.Ok();
	});
```
