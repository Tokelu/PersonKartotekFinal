using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class PersonAddressRelations
    {
        public virtual int AddressID { get; set; }
        public virtual int PersonID { get; set; }

        public virtual Address Address { get; set; }
        public virtual Person Person { get; set; }


    }
}
