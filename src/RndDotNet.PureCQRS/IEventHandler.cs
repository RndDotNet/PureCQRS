namespace RndDotNet.PureCQRS.Example.CQRS;

public interface IEventHandler<in T>
{
	ValueTask Handle(T @event, CancellationToken token);
}
