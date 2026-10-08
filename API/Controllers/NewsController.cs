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
    public class NewsController : ControllerBase
    {
        private readonly INewsBusiness _newsBusiness;

        public NewsController(INewsBusiness newsBusiness)
        {
            _newsBusiness = newsBusiness;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] NewsModel model)
        {
            var result = _newsBusiness.Create(model);
            return Ok(new { success = result });
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody] NewsModel model)
        {
            var result = _newsBusiness.Update(model);
            return Ok(new { success = result });
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetDatabyID(int id)
        {
            var data = _newsBusiness.GetDatabyID(id);
            return Ok(data);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var result = _newsBusiness.Delete(id);
            return Ok(new { success = result });
        }

        [HttpPost("search")]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                int pageIndex = int.Parse(formData["pageIndex"].ToString());
                int pageSize = int.Parse(formData["pageSize"].ToString());
                string title = formData.Keys.Contains("title") ? Convert.ToString(formData["title"]) : null;

                long total = 0;
                var data = _newsBusiness.Search(pageIndex, pageSize, out total, title);
                return Ok(new { TotalItems = total, Data = data, PageIndex = pageIndex, PageSize = pageSize });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}