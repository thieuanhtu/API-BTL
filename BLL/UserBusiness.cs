using DAL;
using DAL.Interfaces;
using Model;
using System.Collections.Generic;
using BCrypt.Net;
using System;

namespace BLL
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository _userRepository;

        public UserBusiness(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Create(UserModel model)
        {
            // Hash mật khẩu trước khi lưu
            model.matkhau = BCrypt.Net.BCrypt.HashPassword(model.matkhau);
            return _userRepository.Create(model);
        }
        public bool Register(UserModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.hoten))
                throw new ArgumentException("Họ tên không được để trống");

            model.taikhoan = (model.taikhoan ?? "").Trim();
            if (model.taikhoan.Length < 4 || model.taikhoan.Length > 30)
                throw new ArgumentException("Tài khoản phải từ 4 đến 30 ký tự");

            if (string.IsNullOrEmpty(model.matkhau) || model.matkhau.Length < 6 || model.matkhau.Length > 72)
                throw new ArgumentException("Mật khẩu phải từ 6 đến 72 ký tự");

            if (model.ngaysinh == null)
                throw new ArgumentException("Vui lòng nhập ngày sinh");

            if (_userRepository.GetByUsername(model.taikhoan) != null)
                throw new ArgumentException("Tài khoản đã tồn tại");

            model.user_id = "U" + DateTime.Now.ToString("yyMMddHHmmss");
            model.role = "PhucVu";                 // luôn là quyền thấp nhất, không tin client
            model.diachi = model.diachi ?? "";
            model.gioitinh = model.gioitinh ?? "";
            model.email = model.email ?? "";
            model.image_url = "";

            return Create(model);                  // Create sẽ băm mật khẩu
        }

        public bool Update(UserModel model)
        {
            // Nếu người dùng có đổi mật khẩu, hash lại; nếu không đổi, giữ nguyên hash cũ
            if (!string.IsNullOrEmpty(model.matkhau))
            {
                model.matkhau = BCrypt.Net.BCrypt.HashPassword(model.matkhau);
            }
            return _userRepository.Update(model);
        }

        public bool Delete(string id) => _userRepository.Delete(id);

        public UserModel GetDatabyID(string id) => _userRepository.GetDatabyID(id);

        public List<UserModel> Search(int pageIndex, int pageSize, out long total, string hoten, string taikhoan)
            => _userRepository.Search(pageIndex, pageSize, out total, hoten, taikhoan);

        public UserModel Authenticate(string username, string password)
        {
            var user = _userRepository.GetByUsername(username);
            if (user == null) return null;

            // So sánh mật khẩu người dùng nhập với hash đã lưu
            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.matkhau);
            if (!isValid) return null;

            user.matkhau = null; // không trả mật khẩu/hash về client
            return user;
        }
    }
}