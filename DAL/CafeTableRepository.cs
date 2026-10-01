using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class CafeTableRepository : ICafeTableRepository
    {
        private readonly IDatabaseHelper _dbHelper;

        public CafeTableRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public CafeTableModel GetDatabyID(string id)
        {
            string msgError = "";
            var dt = _dbHelper.ExecuteQuery("sp_cafe_table_get_by_id", out msgError, "@table_id", id);
            if (!string.IsNullOrEmpty(msgError)) throw new Exception(msgError);

            var table = new CafeTableModel();
            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                table.table_id = row["table_id"].ToString();
                table.table_name = row["table_name"].ToString();
                table.status = row["status"].ToString();
            }
            return table;
        }

        public bool Create(CafeTableModel model)
        {
            string msgError = "";
            var result = _dbHelper.ExecuteScalar("sp_cafe_table_create", out msgError,
                "@table_id", model.table_id,
                "@table_name", model.table_name,
                "@status", model.status);

            if (!string.IsNullOrEmpty(msgError)) return false;
            return true;
        }

        public bool Update(CafeTableModel model)
        {
            string msgError = "";
            var result = _dbHelper.ExecuteScalar("sp_cafe_table_update", out msgError,
                "@table_id", model.table_id,
                "@table_name", model.table_name,
                "@status", model.status);

            if (!string.IsNullOrEmpty(msgError)) return false;
            return true;
        }

        public List<CafeTableModel> GetDataAll()
        {
            string msgError = "";
            var dt = _dbHelper.ExecuteQuery("sp_cafe_table_get_data_all", out msgError);
            if (!string.IsNullOrEmpty(msgError)) throw new Exception(msgError);

            var list = new List<CafeTableModel>();
            foreach (DataRow row in dt.Rows)
            {
                var table = new CafeTableModel();
                table.table_id = row["table_id"].ToString();
                table.table_name = row["table_name"].ToString();
                table.status = row["status"].ToString();
                list.Add(table);
            }
            return list;
        }
    }
}