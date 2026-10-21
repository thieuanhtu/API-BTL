using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,ThuNgan,PhucVu")]
    public class OrderItemModifierController : ControllerBase
    {
        private readonly IModifierBusiness _modifierBusiness;

        public OrderItemModifierController(IModifierBusiness modifierBusiness)
        {
            _modifierBusiness = modifierBusiness;
        }

        // Gán (ghi đè) danh sách lựa chọn cho 1 dòng món trong hóa đơn
        [HttpPost("set")]
        public IActionResult Set([FromBody] SetRequest request)
        {
            try
            {
                var result = _modifierBusiness.SetForOrderItem(request.maChiTiet, request.listModifierId);
                return Ok(new { success = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("get-by-order-item/{maChiTiet}")]
        public IActionResult GetByOrderItem(string maChiTiet)
        {
            var data = _modifierBusiness.GetByOrderItem(maChiTiet);
            return Ok(data);
        }

        public class SetRequest
        {
            public string maChiTiet { get; set; }
            public List<string> listModifierId { get; set; }
        }
    }
}