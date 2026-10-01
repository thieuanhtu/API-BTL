using Model;
using System.Collections.Generic;

namespace BLL
{
    public interface ICustomerBusiness
    {
        CustomerModel GetDatabyID(string id);
        bool Create(CustomerModel model);
        bool Update(CustomerModel model);
        bool Delete(string id);
        List<CustomerModel> Search(int pageIndex, int pageSize, out long total, string customer_name);
    }
}