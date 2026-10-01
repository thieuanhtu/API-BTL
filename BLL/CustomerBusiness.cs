using DAL;
using Model;
using System.Collections.Generic;

namespace BLL
{
    public class CustomerBusiness : ICustomerBusiness
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerBusiness(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public CustomerModel GetDatabyID(string id) => _customerRepository.GetDatabyID(id);

        public bool Create(CustomerModel model) => _customerRepository.Create(model);

        public bool Update(CustomerModel model) => _customerRepository.Update(model);

        public bool Delete(string id) => _customerRepository.Delete(id);

        public List<CustomerModel> Search(int pageIndex, int pageSize, out long total, string customer_name)
            => _customerRepository.Search(pageIndex, pageSize, out total, customer_name);
    }
}