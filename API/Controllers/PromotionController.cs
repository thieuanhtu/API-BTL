using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Linq;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionBusiness _promotionBusiness;

        public PromotionController(IPromotionBusiness promotionBusiness)
        {
            _promotionBusiness = promotionBusiness;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] PromotionModel model)
        {
            var result = _promotionBusiness.Create(model);
            return Ok(new { success = result });
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody] PromotionModel model)
        {
            var result = _promotionBusiness.Update(model);
            return Ok(new { success = result });
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetDatabyID(string id)
        {
            var data = _promotionBusiness.GetDatabyID(id);
            return Ok(data);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(string id)
        {
            var result = _promotionBusiness.Delete(id);
            return Ok(new { success = result });
        }

        [HttpPost("search")]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                int pageIndex = int.Parse(formData["pageIndex"].ToString());
                int pageSize = int.Parse(formData["pageSize"].ToString());
                string promotionName = formData.Keys.Contains("promotionName") ? Convert.ToString(formData["promotionName"]) : null;

                long total = 0;
                var data = _promotionBusiness.Search(pageIndex, pageSize, out total, promotionName);
                return Ok(new { TotalItems = total, Data = data, PageIndex = pageIndex, PageSize = pageSize });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("apply-to-hoadon")]
        public IActionResult ApplyToHoaDon([FromBody] Dictionary<string, string> formData)
        {
            string maHoaDon = formData["maHoaDon"];
            string promotionId = formData["promotionId"];
            var result = _promotionBusiness.ApplyToHoaDon(maHoaDon, promotionId);
            return Ok(new { success = result });
        }
    }
}