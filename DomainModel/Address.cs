using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Address
    {
        public virtual int AddressID { get; set; }
        public virtual string RoadName { get; set; }
        public virtual string HouseNumber { get; set; }
        public virtual string Story { get; set; }
        public virtual string IsRegisteredAddress { get; set; }
        public virtual string AddressType { get; set; }
        public virtual ICollection<Person> Persons { get; set; }
        public virtual int CityID { get; set; }
        public virtual AddressCity AddressCity { get; set; }
        public virtual List<PersonAddressRelations> PersonRelations { get; set; } = new List<PersonAddressRelations>();


    }
}
