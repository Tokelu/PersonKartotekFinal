using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Notes
    {
        public virtual int NoteID { get; set; }
        public virtual string Note { get; set; }
        public virtual int PersonID { get; set; }
    }
}
