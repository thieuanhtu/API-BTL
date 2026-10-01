using Model;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        bool Create(UserModel model);
        bool Update(UserModel model);
        bool Delete(string id);
        UserModel GetUser(string username, string password);
        UserModel GetDatabyID(string id);
        List<UserModel> Search(int pageIndex, int pageSize, out long total, string hoten, string taikhoan);
        UserModel Authenticate(string username, string password);
    }
}