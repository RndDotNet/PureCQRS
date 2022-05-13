using RndDotNet.PureCQRS.Example.CQRS;
using RndDotNet.PureCQRS.Example.Model;

namespace RndDotNet.PureCQRS.Example.Queries;

public record GetListPostsQuery(int? Limit = 50, int? Offset = 0) : IQuery<List<Post>>;

public sealed class GetListPostsQueryHandler : IQueryHandler<GetListPostsQuery, List<Post>>
{
	public async ValueTask<List<Post>> Handle(GetListPostsQuery query, CancellationToken ct)
	{
		// some logic here
		return new List<Post>();
	}
}
