using Post.Query.Core.Domain;
using Post.Query.Core.Handlers;
using Post.Query.Core.Infrastructure;
using Post.Query.Core.Producers;
using Post.Query.Domain.Aggregrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Infrastructure.Handlers
{
    public class EventSourcingHandler : IEventSourcingHandler<PostAggregate>
    {
        private readonly IEventStore _ieventStore;
        private readonly IEventProducer _ieventProducer;
        public EventSourcingHandler(IEventStore ieventStore, IEventProducer eventProducer)
        {
            _ieventStore = ieventStore ?? throw new ArgumentNullException(nameof(ieventStore));
            _ieventProducer = eventProducer ?? throw new ArgumentNullException(nameof(eventProducer));
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
            var aggregateIds = await _ieventStore.GetAggregateIdsAsync();

            if (aggregateIds == null || !aggregateIds.Any()) return;

            foreach (var aggregateId in aggregateIds)
            {
                var aggregate = await GetByIdAsync(aggregateId);

                if (aggregate == null || !aggregate.Active) continue;

                var events = await _ieventStore.GetEventsAsync(aggregateId);

                foreach (var @event in events)
                {
                    var topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC");
                    await _ieventProducer.ProduceAsync(topic, @event);
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
