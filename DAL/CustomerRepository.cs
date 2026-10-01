using DAL.Helper;
using DAL.Helper.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDatabaseHelper _dbHelper;

        public CustomerRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public bool Create(CustomerModel model)
        {
            string msgError = "";
            var result = _dbHelper.ExecuteScalar("sp_customer_create", out msgError,
                "@customer_id", model.customer_id,
                "@customer_name", model.customer_name,
                "@phone", model.phone,
                "@address", model.address);

            if (!string.IsNullOrEmpty(msgError)) return false;
            return true;
        }

        public bool Update(CustomerModel model)
        {
            string msgError = "";
            var result = _dbHelper.ExecuteScalar("sp_customer_update", out msgError,
                "@customer_id", model.customer_id,
                "@customer_name", model.customer_name,
                "@phone", model.phone,
                "@address", model.address);

            if (!string.IsNullOrEmpty(msgError)) return false;
            return true;
        }

        public bool Delete(string id)
        {
            string msgError = "";
            var result = _dbHelper.ExecuteScalar("sp_customer_delete", out msgError,
                "@customer_id", id);

            if (!string.IsNullOrEmpty(msgError)) return false;
            return true;
        }

        public CustomerModel GetDatabyID(string id)
        {
            string msgError = "";
            var dt = _dbHelper.ExecuteQuery("sp_customer_get_by_id", out msgError, "@customer_id", id);
            if (!string.IsNullOrEmpty(msgError)) throw new Exception(msgError);

            var cus = new CustomerModel();
            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                cus.customer_id = row["customer_id"].ToString();
                cus.customer_name = row["customer_name"].ToString();
                cus.phone = row["phone"].ToString();
                cus.address = row["address"].ToString();
            }
            return cus;
        }

        public List<CustomerModel> Search(int pageIndex, int pageSize, out long total, string customer_name)
        {
            string msgError = "";
            total = 0;
            var dt = _dbHelper.ExecuteQuery("sp_customer_search", out msgError,
                "@page_index", pageIndex,
                "@page_size", pageSize,
                "@customer_name", customer_name);

            if (!string.IsNullOrEmpty(msgError)) throw new Exception(msgError);

            var list = new List<CustomerModel>();
            if (dt.Rows.Count > 0)
            {
                total = Convert.ToInt64(dt.Rows[0]["RecordCount"]);
                foreach (DataRow row in dt.Rows)
                {
                    var cus = new CustomerModel();
                    cus.customer_id = row["customer_id"].ToString();
                    cus.customer_name = row["customer_name"].ToString();
                    cus.phone = row["phone"].ToString();
                    cus.address = row["address"].ToString();
                    list.Add(cus);
                }
            }
            return list;
        }
    }
}