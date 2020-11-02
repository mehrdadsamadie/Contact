using Contact.Entities.Model;
using Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Constructure
{
   public class RepositoryWrapper: IRepositoryWrapper
    {
        private ContactContext _contactContext;
        private IContactInfoRepository _contactInfo;
        private IAddressRepository _address;
        public IContactInfoRepository ContactInfo
        {
            get
            {
                if (_contactInfo == null)
                {
                    _contactInfo = new ContactInfoRepository(_contactContext);
                }

                return _contactInfo;
            }
        }

        public IAddressRepository Address
        {
            get
            {
                if (_address == null)
                {
                    _address = new AddressRepository(_contactContext);
                }

                return _address;
            }
        }

        public RepositoryWrapper(ContactContext repositoryContext)
        {
            _contactContext = repositoryContext;
        }

        public void Save()
        {
            _contactContext.SaveChanges();
        }
    }
}
