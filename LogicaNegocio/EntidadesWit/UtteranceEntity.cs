using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesWit
{
    public class UtteranceEntity
    {
        public string entity { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public string body { get; set; }
        public List<Entitie> entities { get; set; }

        public UtteranceEntity(string entity, int start, int end, string body, List<Entitie> entities)
        {
            this.entity = entity;
            this.start = start;
            this.end = end;
            this.body = body;
            this.entities = entities;
        }
        public UtteranceEntity() { }
    }

}
