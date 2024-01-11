
using Post.Query.Core.Commands;

namespace Post.API.Commands
{
    public class EditMessageCommand : BaseCommand
    {
        public string Message { get; set; }
    }
}