using Post.Query.Core.Domain;
using System;
using System.Threading.Tasks;

namespace Post.Query.Core.Handlers
{
    public interface IEventSourcingHandler<T>
    {
        Task SaveAsync(AggregateRoot aggregate);
        Task<T> GetByIdAsync(Guid id);

        Task RepublishEventsAsync();
    }
}