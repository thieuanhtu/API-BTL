using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,ThuNgan,PhucVu")]
    public class HoaDonController : ControllerBase
    {
        private readonly IHoaDonBusiness _hoaDonBusiness;

        public HoaDonController(IHoaDonBusiness hoaDonBusiness)
        {
            _hoaDonBusiness = hoaDonBusiness;
        }
        [Authorize(Roles = "Admin,ThuNgan,PhucVu")]
        [HttpPost("create")]
        public IActionResult Create([FromBody] HoaDonModel model)
        {
            var result = _hoaDonBusiness.Create(model);
            return Ok(new { success = result });
        }
        [Authorize(Roles = "Admin,ThuNgan,PhucVu")]
        [HttpPost("update")]
        public IActionResult Update([FromBody] HoaDonModel model)
        {
            var result = _hoaDonBusiness.Update(model);
            return Ok(new { success = result });
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetDatabyID(string id)
        {
            var data = _hoaDonBusiness.GetDatabyID(id);
            return Ok(data);
        }
        [Authorize(Roles = "Admin,ThuNgan")]
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(string id)
        {
            var result = _hoaDonBusiness.Delete(id);
            return Ok(new { success = result });
        }

        [HttpPost("search")]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                int pageIndex = int.Parse(formData["pageIndex"].ToString());
                int pageSize = int.Parse(formData["pageSize"].ToString());
                string hoten = formData.Keys.Contains("hoten") ? Convert.ToString(formData["hoten"]) : null;
                string diachi = formData.Keys.Contains("diachi") ? Convert.ToString(formData["diachi"]) : null;

                long total = 0;
                var data = _hoaDonBusiness.Search(pageIndex, pageSize, out total, hoten, diachi);
                return Ok(new { TotalItems = total, Data = data, PageIndex = pageIndex, PageSize = pageSize });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Admin,ThuNgan,PhucVu")]
        [HttpPost("transfer-table")]
        public IActionResult TransferTable([FromBody] Dictionary<string, string> formData)
        {
            string maHoaDon = formData["maHoaDon"];
            string newTableId = formData["newTableId"];
            var result = _hoaDonBusiness.TransferTable(maHoaDon, newTableId);
            return Ok(new { success = result });
        }
        [Authorize(Roles = "Admin,ThuNgan,PhucVu")]
        [HttpPost("merge")]
        public IActionResult Merge([FromBody] Dictionary<string, string> formData)
        {
            string maHoaDonMain = formData["maHoaDonMain"];
            string maHoaDonSub = formData["maHoaDonSub"];
            var result = _hoaDonBusiness.Merge(maHoaDonMain, maHoaDonSub);
            return Ok(new { success = result });
        }
        [Authorize(Roles = "Admin,ThuNgan,PhucVu")]
        [HttpPost("split")]
        public IActionResult Split([FromBody] SplitRequest request)
        {
            var result = _hoaDonBusiness.Split(request.maHoaDonSource, request.maHoaDonNew, request.newTableId, request.listMaChiTiet);
            return Ok(new { success = result });
        }

        // Thanh toán: trừ kho theo công thức + ghi phiếu xuất kho + đổi trạng thái hóa đơn
        [Authorize(Roles = "Admin,ThuNgan")]
        [HttpPost("pay")]
        public IActionResult Pay([FromBody] Dictionary<string, string> formData)
        {
            try
            {
                string maHoaDon = formData["maHoaDon"];
                var result = _hoaDonBusiness.Pay(maHoaDon);
                return Ok(new { success = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        public class SplitRequest
        {
            public string maHoaDonSource { get; set; }
            public string maHoaDonNew { get; set; }
            public string newTableId { get; set; }
            public List<string> listMaChiTiet { get; set; }
        }
    }
}