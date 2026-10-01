using Model;
using System.Collections.Generic;
using System;
using System.Text;

namespace DAL.Interfaces
{
    public interface ICafeTableRepository
    {
        CafeTableModel GetDatabyID(string id);
        bool Create(CafeTableModel model);
        bool Update(CafeTableModel model);
        List<CafeTableModel> GetDataAll();
    }
}