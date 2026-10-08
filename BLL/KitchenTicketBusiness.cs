using DAL;
using DAL.Interfaces;
using Model;
using System.Collections.Generic;

namespace BLL
{
    public class KitchenTicketBusiness : IKitchenTicketBusiness
    {
        private readonly IKitchenTicketRepository _kitchenTicketRepository;

        public KitchenTicketBusiness(IKitchenTicketRepository kitchenTicketRepository)
        {
            _kitchenTicketRepository = kitchenTicketRepository;
        }

        public bool Create(KitchenTicketModel model) => _kitchenTicketRepository.Create(model);

        public bool UpdateStatus(string ticketId, string status) => _kitchenTicketRepository.UpdateStatus(ticketId, status);

        public KitchenTicketModel GetDatabyID(string id) => _kitchenTicketRepository.GetDatabyID(id);

        public List<KitchenTicketModel> Search(int pageIndex, int pageSize, out long total, string station, string status)
            => _kitchenTicketRepository.Search(pageIndex, pageSize, out total, station, status);
    }
}