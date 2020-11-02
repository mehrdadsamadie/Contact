using Contact.Entities.Model;
using Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Constructure
{
    public class ContactInfoRepository : RepositoryBase<ContactInfo>, IContactInfoRepository
    {
        public ContactInfoRepository(ContactContext contactContext) : base(contactContext)
        {
        }
    }
}