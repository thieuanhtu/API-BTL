using API.Hubs;
using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Model;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Bep,PhucVu")]
    public class KitchenTicketController : ControllerBase
    {
        private readonly IKitchenTicketBusiness _kitchenTicketBusiness;
        private readonly IHubContext<KitchenHub> _hub;

        public KitchenTicketController(IKitchenTicketBusiness kitchenTicketBusiness, IHubContext<KitchenHub> hub)
        {
            _kitchenTicketBusiness = kitchenTicketBusiness;
            _hub = hub;
        }

        [Authorize(Roles = "Admin,ThuNgan,PhucVu")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] KitchenTicketModel model)
        {
            var result = _kitchenTicketBusiness.Create(model);
            if (result)
            {
                // Báo cho tất cả màn hình đang nối: có phiếu mới
                await _hub.Clients.All.SendAsync("TicketCreated", model);
            }
            return Ok(new { success = result });
        }

        [Authorize(Roles = "Admin,Bep")]
        [HttpPost("update-status")]
        public async Task<IActionResult> UpdateStatus([FromBody] Dictionary<string, string> formData)
        {
            string ticketId = formData["ticketId"];
            string status = formData["status"];
            var result = _kitchenTicketBusiness.UpdateStatus(ticketId, status);
            if (result)
            {
                // Báo cho tất cả màn hình đang nối: phiếu đổi trạng thái
                await _hub.Clients.All.SendAsync("TicketStatusChanged", new { ticketId, status });
            }
            return Ok(new { success = result });
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetDatabyID(string id)
        {
            var data = _kitchenTicketBusiness.GetDatabyID(id);
            return Ok(data);
        }

        [HttpPost("search")]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                int pageIndex = int.Parse(formData["pageIndex"].ToString());
                int pageSize = int.Parse(formData["pageSize"].ToString());
                string station = formData.Keys.Contains("station") ? Convert.ToString(formData["station"]) : null;
                string status = formData.Keys.Contains("status") ? Convert.ToString(formData["status"]) : null;

                long total = 0;
                var data = _kitchenTicketBusiness.Search(pageIndex, pageSize, out total, station, status);
                return Ok(new { TotalItems = total, Data = data, PageIndex = pageIndex, PageSize = pageSize });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}