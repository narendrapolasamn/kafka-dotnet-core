
using Post.Query.Core.Commands;

namespace Post.API.Commands
{
    public class EditPostCommand : BaseCommand
    {
        public string Message { get; set; }
    }
}
