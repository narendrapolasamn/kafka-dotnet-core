using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Query.Core.Consumer
{
    public interface IEventConsumer
    {
        void Consume(string topic);
    }
}
