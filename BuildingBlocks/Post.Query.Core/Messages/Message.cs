using System;

namespace Post.Query.Core.Messages
{
    public abstract class Message
    {
        public Guid Id { get; set; }
    }
}