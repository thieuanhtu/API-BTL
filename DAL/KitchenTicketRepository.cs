using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class KitchenTicketRepository : IKitchenTicketRepository
    {
        private readonly IDatabaseHelper _dbHelper;

        public KitchenTicketRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public bool Create(KitchenTicketModel model)
        {
            string msgError = "";
            _dbHelper.ExecuteScalar("sp_kitchen_ticket_create", out msgError,
                "@ticket_id", model.ticket_id,
                "@ma_hoa_don", model.ma_hoa_don,
                "@station", model.station);
            return string.IsNullOrEmpty(msgError);
        }

        public bool UpdateStatus(string ticketId, string status)
        {
            string msgError = "";
            _dbHelper.ExecuteScalar("sp_kitchen_ticket_update_status", out msgError,
                "@ticket_id", ticketId,
                "@status", status);
            return string.IsNullOrEmpty(msgError);
        }

        public KitchenTicketModel GetDatabyID(string id)
        {
            string msgError = "";
            var dt = _dbHelper.ExecuteQuery("sp_kitchen_ticket_get_by_id", out msgError, "@ticket_id", id);
            if (!string.IsNullOrEmpty(msgError)) throw new Exception(msgError);

            var ticket = new KitchenTicketModel();
            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                ticket.ticket_id = row["ticket_id"].ToString();
                ticket.ma_hoa_don = row["ma_hoa_don"].ToString();
                ticket.station = row["station"].ToString();
                ticket.status = row["status"].ToString();
                ticket.created_at = Convert.ToDateTime(row["created_at"]);
            }
            return ticket;
        }

        public List<KitchenTicketModel> Search(int pageIndex, int pageSize, out long total, string station, string status)
        {
            string msgError = "";
            total = 0;
            var dt = _dbHelper.ExecuteQuery("sp_kitchen_ticket_search", out msgError,
                "@page_index", pageIndex,
                "@page_size", pageSize,
                "@station", station,
                "@status", status);

            if (!string.IsNullOrEmpty(msgError)) throw new Exception(msgError);

            var list = new List<KitchenTicketModel>();
            if (dt.Rows.Count > 0)
            {
                total = Convert.ToInt64(dt.Rows[0]["RecordCount"]);
                foreach (DataRow row in dt.Rows)
                {
                    var ticket = new KitchenTicketModel();
                    ticket.ticket_id = row["ticket_id"].ToString();
                    ticket.ma_hoa_don = row["ma_hoa_don"].ToString();
                    ticket.station = row["station"].ToString();
                    ticket.status = row["status"].ToString();
                    ticket.created_at = Convert.ToDateTime(row["created_at"]);
                    list.Add(ticket);
                }
            }
            return list;
        }
    }
}