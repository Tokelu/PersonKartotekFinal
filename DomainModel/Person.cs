using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Person
    {
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ContactType { get; set; }
        //public string Note { get; set; }
        public virtual ICollection<PersonAddressRelations> AddressRelations { get; set; } = new List<PersonAddressRelations>();
        public ICollection<EmailAddr> EmailAddresses { get; set; }
        public ICollection<Phone> Phones { get; set; }
    }
}
