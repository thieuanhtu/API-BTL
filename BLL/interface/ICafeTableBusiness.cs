using Model;
using System.Collections.Generic;

namespace BLL
{
    public interface ICafeTableBusiness
    {
        CafeTableModel GetDatabyID(string id);
        bool Create(CafeTableModel model);
        bool Update(CafeTableModel model);
        List<CafeTableModel> GetDataAll();
    }
}