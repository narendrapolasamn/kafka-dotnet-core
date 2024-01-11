using CQRS.Core.Domain;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using Post.Cmd.Domain.Aggregrate;
using Post.Cmd.Infrastructure.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Cmd.Infrastructure.Handlers
{
    public class EventSourcingHandler : IEventSourcingHandler<PostAggregate>
    {
        private readonly IEventStore _ieventStore;

        public EventSourcingHandler(IEventStore ieventStore)
        {
            _ieventStore = ieventStore ?? throw new ArgumentNullException(nameof(ieventStore));
        }
        public async Task<PostAggregate> GetByIdAsync(Guid id)
        {
            var aggregate = new PostAggregate();
            var events = await _ieventStore.GetEventsAsync(id);
            if (events is null || !events.Any()) return aggregate;
            aggregate.ReplyEvents(events);
            aggregate.Version = events.Select(e => e.Version).Max();
            return aggregate;
        }

        public async Task RepublishEventsAsync()
        {
            var aggregateIds = await _eventStore.GetAggregateIdsAsync();

            if (aggregateIds == null || !aggregateIds.Any()) return;

            foreach (var aggregateId in aggregateIds)
            {
                var aggregate = await GetByIdAsync(aggregateId);

                if (aggregate == null || !aggregate.Active) continue;

                var events = await _eventStore.GetEventsAsync(aggregateId);

                foreach (var @event in events)
                {
                    var topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC");
                    await _eventProducer.ProduceAsync(topic, @event);
                }
            }
        }


        public async Task SaveAsync(AggregateRoot aggregate)
        {
            await _ieventStore.SaveEventsAsync(aggregate.Id, aggregate.GetUncommitedChanges(),aggregate.Version);
            aggregate.MarkChangesAsCommited();
        }
    }
}
