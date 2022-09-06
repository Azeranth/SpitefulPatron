using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpitefulPatron
{
    public class Protocol : Field
    {
        public Protocol()
        {
            length = delegate () { return fields.Sum(n => n.Length()); };
            content = delegate () { return fields.SelectMany(n => n.Content()).ToArray(); };
        }

        protected List<Field> fields = new List<Field>();

        public List<Field> Fields { get => fields; set => fields = value; }
    }
}
