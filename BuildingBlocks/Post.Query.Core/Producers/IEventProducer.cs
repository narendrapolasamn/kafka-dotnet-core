using Post.Query.Core.Events;
using System.Threading.Tasks;

namespace Post.Query.Core.Producers
{
    public interface IEventProducer
    {
        Task ProduceAsync<T>(string topic, T @event) where T :BaseEvent;
    }
}