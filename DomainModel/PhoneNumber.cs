using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Phone
    {
        public virtual int PhoneID { get; set; }
        public virtual string Provider { get; set; }
        public virtual string PhoneType { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual int PersonID { get; set; }
    }
}
