namespace RndDotNet.PureCQRS.Example.CQRS;

public interface ICommandHandler<in TCommand>
{
	ValueTask Handle(TCommand command, CancellationToken ct);
}
