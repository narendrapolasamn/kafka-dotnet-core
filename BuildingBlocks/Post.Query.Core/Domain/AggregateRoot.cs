using Post.Query.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Query.Core.Domain
{
    public abstract class AggregateRoot
    {
        protected Guid _id;

        protected readonly List<BaseEvent> _changes = new();

        public Guid Id
        {
            get { return _id; }
        }

        public int Version { get; set; } = -1;

        public List<BaseEvent> GetUncommitedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommited()
        {
            _changes.Clear();
        }

        private void ApplyChange(BaseEvent @event, bool isNew) 
        {
            var method = this.GetType().GetMethod("Apply", new Type[] { @event.GetType() });
            if(method is null ) {
                throw new ArgumentNullException(nameof(method),
                            $"The Apply method was not found in the Aggregate gor {@event.GetType().Name}");
            }

            method.Invoke( this, new object[] { @event});
            if(isNew)
            {
                _changes.Add(@event);
            }
        }

        public void RaiseEvent(BaseEvent @event)
        {
            ApplyChange(@event, true);
        }
        public void ReplyEvents(IEnumerable<BaseEvent> events)
        {
            foreach(var @event in events)
            {
                ApplyChange(@event, false);
            }
        }
    }
}
