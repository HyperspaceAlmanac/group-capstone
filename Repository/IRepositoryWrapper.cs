using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalService.Repository
{
    interface IRepositoryWrapper
    {
        public ICustomerRepository Customer { get; }
        void Save();
    }
}
