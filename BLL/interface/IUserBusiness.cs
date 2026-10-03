using Model;
using System.Collections.Generic;

namespace BLL
{
    public interface IUserBusiness
    {
        bool Create(UserModel model);
        bool Update(UserModel model);
        bool Delete(string id);
        UserModel GetDatabyID(string id);
        List<UserModel> Search(int pageIndex, int pageSize, out long total, string hoten, string taikhoan);
        UserModel Authenticate(string username, string password);
    }
}