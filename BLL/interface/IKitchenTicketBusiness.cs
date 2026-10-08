using Model;
using System.Collections.Generic;

namespace BLL
{
    public interface IKitchenTicketBusiness
    {
        bool Create(KitchenTicketModel model);
        bool UpdateStatus(string ticketId, string status);
        KitchenTicketModel GetDatabyID(string id);
        List<KitchenTicketModel> Search(int pageIndex, int pageSize, out long total, string station, string status);
    }
}