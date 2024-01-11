using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Post.Query.Core.Events;

namespace Post.Query.Core.Domain
{
    public interface IEventStoreRepository
    {
        Task SaveAsync(EventModel @event);
        Task<List<EventModel>> FindByAggregateId(Guid aggregateId);

        Task<List<EventModel>> FindAllAsync();
    }
}