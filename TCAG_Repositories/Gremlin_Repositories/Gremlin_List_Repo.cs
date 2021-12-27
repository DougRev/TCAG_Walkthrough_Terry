using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCAG_Repositories.Entities;

namespace TCAG_Repositories.Gremlin_Repositories
{
    public class Gremlin_List_Repo
    {
        private readonly List<Gremlin> _gremlinDbContext = new List<Gremlin>();
        

        private int _count;
        public bool AddGremlinToDatabase(Gremlin gremlin)
        {
            if (gremlin is null)
            {
                return false;
            }
            else
            {
                _count++;
                gremlin.ID = _count;
                _gremlinDbContext.Add(gremlin);
                return true;
            }
        }
        public List<Gremlin> GetGremlins()
        {
            return _gremlinDbContext;
        }
        public Gremlin GetGremlin(int gremlinID)
        {
            foreach (var gremlin in _gremlinDbContext)
            {
                if (gremlin.ID == gremlinID)
                {
                    return gremlin;
                }
            }
            return null;
        }

        public bool UpdateGremlinData(int gremlinID, Gremlin newGremlinData)
        {
            var oldGremlinData = GetGremlin(gremlinID);

            if (oldGremlinData != null)
            {
                oldGremlinData.Name = newGremlinData.Name;
                oldGremlinData.GremlinType = newGremlinData.GremlinType;
                return true;
            }
            return false;
        }

        public bool DeleteGremlin(int gremlinID)
        {
            foreach (var gremlin in _gremlinDbContext)
            {
                if (gremlin.ID == gremlinID)
                {
                    _gremlinDbContext.Remove(gremlin);
                    return true;
                }
            }
            return false;
        }
    }
}
