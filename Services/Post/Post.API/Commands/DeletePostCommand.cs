

using Post.Query.Core.Commands;

namespace Post.API.Commands
{
    public class DeletePostCommand : BaseCommand
    {
        public Guid CommentId { get; set; }
        public string Username { get; set; }
    }
}
