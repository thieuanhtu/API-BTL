using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseHelper _dbHelper;

        public UserRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public bool Create(UserModel model)
        {
            string msgError = "";
            _dbHelper.ExecuteScalar("sp_user_create", out msgError,
                "@user_id", model.user_id,
                "@hoten", model.hoten,
                "@ngaysinh", model.ngaysinh,
                "@diachi", model.diachi,
                "@gioitinh", model.gioitinh,
                "@email", model.email,
                "@taikhoan", model.taikhoan,
                "@matkhau", model.matkhau,
                "@role", model.role,
                "@image_url", model.image_url);
            return string.IsNullOrEmpty(msgError);
        }

        public bool Update(UserModel model)
        {
            string msgError = "";
            _dbHelper.ExecuteScalar("sp_user_update", out msgError,
                "@user_id", model.user_id,
                "@hoten", model.hoten,
                "@ngaysinh", model.ngaysinh,
                "@diachi", model.diachi,
                "@gioitinh", model.gioitinh,
                "@email", model.email,
                "@taikhoan", model.taikhoan,
                "@matkhau", model.matkhau,
                "@role", model.role,
                "@image_url", model.image_url);
            return string.IsNullOrEmpty(msgError);
        }

        public bool Delete(string id)
        {
            string msgError = "";
            _dbHelper.ExecuteScalar("sp_user_delete", out msgError, "@user_id", id);
            return string.IsNullOrEmpty(msgError);
        }

        public UserModel GetUser(string username, string password)
        {
            string msgError = "";
            var dt = _dbHelper.ExecuteQuery("sp_user_get_by_username_password", out msgError,
                "@taikhoan", username, "@matkhau", password);
            if (!string.IsNullOrEmpty(msgError)) throw new Exception(msgError);

            var user = new UserModel();
            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                user.user_id = row["user_id"].ToString();
                user.hoten = row["hoten"].ToString();
                user.taikhoan = row["taikhoan"].ToString();
                user.role = row["role"].ToString();
            }
            return user;
        }

        public UserModel GetDatabyID(string id)
        {
            string msgError = "";
            var dt = _dbHelper.ExecuteQuery("sp_user_get_by_id", out msgError, "@user_id", id);
            if (!string.IsNullOrEmpty(msgError)) throw new Exception(msgError);

            var user = new UserModel();

            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                user.user_id = row["user_id"].ToString();
                user.hoten = row["hoten"].ToString();
                user.taikhoan = row["taikhoan"].ToString();
            }
            return user;
        }

        public List<UserModel> Search(int pageIndex, int pageSize, out long total, string hoten, string taikhoan)
        {
            string msgError = "";
            total = 0;
            var dt = _dbHelper.ExecuteQuery("sp_user_search", out msgError,
                "@page_index", pageIndex,
                "@page_size", pageSize,
                "@hoten", hoten,
                "@taikhoan", taikhoan);

            if (!string.IsNullOrEmpty(msgError)) throw new Exception(msgError);

            var list = new List<UserModel>();
            if (dt.Rows.Count > 0)
            {
                total = Convert.ToInt64(dt.Rows[0]["RecordCount"]);
                foreach (DataRow row in dt.Rows)
                {
                    var user = new UserModel();
                    user.user_id = row["user_id"].ToString();
                    user.hoten = row["hoten"].ToString();
                    user.taikhoan = row["taikhoan"].ToString();
                    list.Add(user);
                }
            }
            return list;
        }

        public UserModel Authenticate(string username, string password)
        {
            return GetUser(username, password);
        }
    }
}