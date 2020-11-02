using Contact.Entities.Model;
using Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Constructure
{
    public class AddressRepository : RepositoryBase<Address>, IAddressRepository
    {
        public AddressRepository(ContactContext contactContext) : base(contactContext)
        {
        }
    }

}
