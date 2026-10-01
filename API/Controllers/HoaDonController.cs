using BLL;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly IHoaDonBusiness _hoaDonBusiness;

        public HoaDonController(IHoaDonBusiness hoaDonBusiness)
        {
            _hoaDonBusiness = hoaDonBusiness;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] HoaDonModel model)
        {
            var result = _hoaDonBusiness.Create(model);
            return Ok(new { success = result });
        }

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
    }
}