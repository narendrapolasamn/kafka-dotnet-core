
using Post.Query.Core.Messages;

namespace Post.Query.Core.Events
{
    public abstract class BaseEvent : Message
    {
        protected BaseEvent(string type)
        {
            Type = type;
        }

        public string Type { get; set; }
        public int Version { get; set; }
    }
}