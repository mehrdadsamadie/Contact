using Repository.Constructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Infrastructure
{
    public interface IRepositoryWrapper
    {
        IContactInfoRepository ContactInfo { get; }
        IAddressRepository Address { get; }
        void Save();
    }
}
