namespace RndDotNet.PureCQRS.Example.CQRS;

public interface IEventHandler<in TEvent>
{
	ValueTask Handle(TEvent e, CancellationToken token);
}
