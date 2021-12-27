using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCAG_Repositories.Entities;

namespace TCAG_Repositories.Gremlin_Repositories
{
   public class Gremlin_Queue_Repo
    {
        private readonly Queue<Gremlin> _gremlinQueue = new Queue<Gremlin>();
        private int _count;
        /*
         * _gremlinDbContext[3] this is legal with a list...
         * but, this doesn't work w/ the queue you can only loop through the queue to see 'EVERYTHING'
         * or you can see whose next in line.
         */
        public bool AddGremlinToQueue(Gremlin gremlin)
        {
            if (gremlin == null)
            {
                return false;
            }
            _count++;
            gremlin.ID = _count;
            _gremlinQueue.Enqueue(gremlin);//enqueue is the add method for a queue
            return true;

        }
        public Queue<Gremlin> GetGremlins()
        {
            return _gremlinQueue;
        }
        public Gremlin ViewNextGremlin()
        {
            if (_gremlinQueue.Count > 0)
            {
                return _gremlinQueue.Peek();
            }
            return null;
        }

        public bool PurchaseGremlin()
        {
            if (_gremlinQueue.Count > 0)
            {
                _gremlinQueue.Dequeue();
                return true;
            }
            return false;
        }
    }
}
