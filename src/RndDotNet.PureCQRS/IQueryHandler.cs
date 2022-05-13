namespace RndDotNet.PureCQRS.Example.CQRS;

public interface IQueryHandler<in TQuery, TResult>
{
	ValueTask<TResult> Handle(TQuery query, CancellationToken ct);
}
