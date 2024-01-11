using Post.Query.Core.Queries;

namespace Post.Query.API.Queries
{
    public class FindPostsWithLikesQuery : BaseQuery
    {
        public int NumberOfLikes { get; set; }
    }
}