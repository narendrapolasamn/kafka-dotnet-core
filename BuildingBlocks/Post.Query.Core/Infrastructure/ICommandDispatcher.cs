using Post.Query.Core.Commands;
using System;
using System.Threading.Tasks;

namespace Post.Query.Core.Infrastructure
{
    public interface ICommandDispatcher
    {
        void RegisterHandler<T>(Func<T, Task> handler) where T : BaseCommand;
        Task SendAsync(BaseCommand command);
    }
}