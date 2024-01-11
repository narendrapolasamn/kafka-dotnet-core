
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Post.Infrastructure.Config;
using Post.Query.Core.Domain;
using Post.Query.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Infrastructure.Repositories
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly IMongoCollection<EventModel> _events;

        public EventStoreRepository(IOptions<MongoDbConfig> config)
        {
            var mongoClient = new MongoClient(config.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(config.Value.Database);
            _events = mongoDatabase.GetCollection<EventModel>(config.Value.Collection);

        }
        public async Task<List<EventModel>> FindAllAsync()
        {
            return await _events.Find(_ => true).ToListAsync().ConfigureAwait(false);
        }

        public async Task<List<EventModel>> FindByAggregateId(Guid aggregateId)
        {
           return await _events.Find(x=>x.AggregateIdentifier == aggregateId).ToListAsync().ConfigureAwait(false);
        }

        public async Task SaveAsync(EventModel @event)
        {
            await _events.InsertOneAsync(@event).ConfigureAwait(false);
        }
    }
}
