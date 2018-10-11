﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class AddressCity
    {
        public int CityID { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }

        public ICollection<Address> Addresses;
    }
}
