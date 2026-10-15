using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CafeTableController : ControllerBase
    {
        private readonly ICafeTableBusiness _cafeTableBusiness;

        public CafeTableController(ICafeTableBusiness cafeTableBusiness)
        {
            _cafeTableBusiness = cafeTableBusiness;
        }

        // Sơ đồ bàn: ai đăng nhập cũng xem được
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(_cafeTableBusiness.GetDataAll());
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetDatabyID(string id)
        {
            return Ok(_cafeTableBusiness.GetDatabyID(id));
        }

        // Thêm bàn mới: chỉ Admin
        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public IActionResult Create([FromBody] CafeTableModel model)
        {
            try
            {
                var result = _cafeTableBusiness.Create(model);
                return Ok(new { success = result });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // Sửa tên bàn / đổi trạng thái bàn (Trống, Đang phục vụ, Đã đặt)
        [Authorize(Roles = "Admin,ThuNgan,PhucVu")]
        [HttpPost("update")]
        public IActionResult Update([FromBody] CafeTableModel model)
        {
            try
            {
                var result = _cafeTableBusiness.Update(model);
                return Ok(new { success = result });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}