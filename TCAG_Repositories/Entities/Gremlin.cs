using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCAG_Repositories.Entities.Enums;

namespace TCAG_Repositories.Entities
{
    public class Gremlin
    {
        public Gremlin(string name, GremlinType gremlintype)
        {
            Name = name;
            GremlinType = gremlintype;
        }
        public Gremlin()
        {

        }
        public int ID { get; set; }
        public string Name { get; set; }

        public GremlinType GremlinType { get; set; }
        public decimal GremlinValue 
        {
            get
            {
                decimal value = 0;
            return value = GetGremlinValue(GremlinType);
            }
        }

        private decimal GetGremlinValue(GremlinType gremlinType)
        {
            switch (gremlinType)
            {
                case GremlinType.King:
                    return 10000m;
                   
                case GremlinType.Enforcer:
                    return 5000;
                    
                case GremlinType.Pawn:
                    return 1000;
                default:
                    return 0;
                    
            }
        }
    }
}
