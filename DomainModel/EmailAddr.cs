using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class EmailAddr
    {
        public virtual int EmailID { get; set; }
        public virtual bool IsPrimaryEmail { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual int PersonID { get; set; }
    }
}
